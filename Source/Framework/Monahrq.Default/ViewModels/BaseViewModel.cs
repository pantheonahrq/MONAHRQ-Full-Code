﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System.IO;
using System.Threading.Tasks;
using Monahrq.Infrastructure;
using Monahrq.Infrastructure.Extensions;
using Monahrq.Sdk.Logging;

namespace Monahrq.Default.ViewModels
{
    /// <summary>
    ///     Base class for view models that maintain "dirty" state (commit) and/or require validation
    ///     Borrowed from Jounce 
    /// </summary>
    /// <remarks>
    ///     The INotifyDataErrorInfo implementation is based on the Prism MVVM quickstart:
    ///     http://compositewpf.codeplex.com/
    /// </remarks>    
    [Obsolete("Please refactor to use BaseNofity instead.", false)]
    public abstract class BaseViewModel : BaseNotify, INotifyDataErrorInfo
    {
        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public IEventAggregator Events
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether [specific non error allow commit].
        /// </summary>
        /// <value>
        /// <c>true</c> if [specific non error allow commit]; otherwise, <c>false</c>.
        /// </value>
        protected virtual bool SpecificNonErrorAllowCommit
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        protected BaseViewModel()
        {
            CommitCommand = new DelegateCommand<object>(OnCommit, CanCommit);
            Events = ServiceLocator.Current.GetInstance<IEventAggregator>();
            // change validity for commit whenever the error condition changes
            ErrorsChanged += (o, e) => CommitCommand.RaiseCanExecuteChanged();

            // any time a property changes that is not the committed flag, reset committed
            PropertyChanged += (o, e) =>
            {
                if (Committed && !e.PropertyName.Equals(ExtractPropertyName(() => Committed)))
                {
                    Committed = false;
                }
            };
            ViewImportSampleCommand = new DelegateCommand<string>(OnViewSample);
        }

        private readonly Lazy<ILogWriter> logger = new Lazy<ILogWriter>(() => new SessionLogger(new CallbackLogger()));

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        public BaseViewModel(IEventAggregator events)
            : this()
        {
            Events = events;
        }
        /// <summary>
        ///     Use this to validate any late-bound fields before committing
        /// </summary>
        protected virtual void ValidateAll()
        {

        }

        /// <summary>
        ///     Use this to perform whatever action is needed when committed
        /// </summary>
        protected virtual Task<bool> OnCommitted()
        {
            return Task.FromResult(true);
        }

        /// <summary>
        ///     True when changes have been committed
        /// </summary>
        private bool _committed = true;

        /// <summary>
        ///     This value will raise property changed when committed without errors
        /// </summary>
        public bool Committed
        {
            get { return _committed; }
            set
            {
                _committed = value;
                if (CommitCommand != null)
                {
                    CommitCommand.RaiseCanExecuteChanged();
                }
                RaisePropertyChanged(() => Committed);
            }
        }

        /// <summary>
        ///     Commit changes
        /// </summary>
        public DelegateCommand<object> CommitCommand { get; private set; }

        public DelegateCommand<string> ViewImportSampleCommand { get; set; }

        protected ILogWriter Logger => this.logger.Value;

        /// <summary>
        ///     Internal list of errors
        /// </summary>
        private readonly Dictionary<string, IEnumerable<string>> _errors = new Dictionary<string, IEnumerable<string>>();

        /// <summary>
        ///     Collction of errors changed
        /// </summary>
        private event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire object.
        /// </summary>
        event EventHandler<DataErrorsChangedEventArgs> INotifyDataErrorInfo.ErrorsChanged
        {
            [method:
                SuppressMessage("Microsoft.Design", "CA1033", Justification = "Doesn't need to access event methods.")]
            add { ErrorsChanged += value; }
            [method:
                SuppressMessage("Microsoft.Design", "CA1033", Justification = "Doesn't need to access event methods.")]
            remove { ErrorsChanged -= value; }
        }

        /// <summary>
        ///     True if errors exist
        /// </summary>
        bool INotifyDataErrorInfo.HasErrors
        {
            get { return HasErrors; }
        }

        /// <summary>
        ///     True if errors exist
        /// </summary>
        protected bool HasErrors
        {
            get { return _errors.Any(); }
        }

        /// <summary>
        ///     Get errors for a property
        /// </summary>
        /// <param name="propertyName">The property</param>
        /// <returns>The list of errors</returns>
        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return GetErrors(propertyName);
        }

        /// <summary>
        ///     Get errors for a property
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <returns></returns>
        protected virtual IEnumerable GetErrors(string propertyName)
        {
            IEnumerable<string> error;
            return _errors.TryGetValue(propertyName ?? string.Empty, out error) ? error : null;
        }

        /// <summary>
        ///     Set an error for a property
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="error">The error</param>
        protected virtual void SetError(string propertyName, string error)
        {
            SetErrors(propertyName, new List<string> { error });
        }

        /// <summary>
        ///     Overload for expression
        /// </summary>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="propertyExpresssion">An expression that points to the property</param>
        /// <param name="error">The error</param>
        protected virtual void SetError<T>(Expression<Func<T>> propertyExpresssion, string error)
        {
            var propertyName = ExtractPropertyName(propertyExpresssion);
            SetError(propertyName, error);
        }

        /// <summary>
        ///     Clears the errors
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void ClearErrors(string propertyName)
        {
            SetErrors(propertyName, new List<string>());
        }

        /// <summary>
        ///     Clear all errors for a property
        /// </summary>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="propertyExpresssion">The expression that points to the property</param>
        protected virtual void ClearErrors<T>(Expression<Func<T>> propertyExpresssion)
        {
            var propertyName = ExtractPropertyName(propertyExpresssion);
            ClearErrors(propertyName);
        }

        /// <summary>
        ///     Set errors for a property
        /// </summary>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="propertyExpresssion">The expression for the property</param>
        /// <param name="propertyErrors">The collection of errors</param>
        protected virtual void SetErrors<T>(Expression<Func<T>> propertyExpresssion, IEnumerable<string> propertyErrors)
        {
            var propertyName = ExtractPropertyName(propertyExpresssion);
            SetErrors(propertyName, propertyErrors);
        }

        /// <summary>
        ///     Set errors for a property
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="propertyErrors">The collection of errors</param>
        protected virtual void SetErrors(string propertyName, IEnumerable<string> propertyErrors)
        {
            if (propertyErrors.Any(error => error == null))
            {
                throw new ArgumentException(@"Errors messages should not be null", "propertyErrors");
            }

            var propertyNameKey = propertyName ?? string.Empty;

            IEnumerable<string> currentPropertyErrors;
            if (_errors.TryGetValue(propertyNameKey, out currentPropertyErrors))
            {
                if (!_AreErrorCollectionsEqual(currentPropertyErrors, propertyErrors))
                {
                    if (propertyErrors.Any())
                    {
                        _errors[propertyNameKey] = propertyErrors;
                    }
                    else
                    {
                        _errors.Remove(propertyNameKey);
                    }

                    RaiseErrorsChanged(propertyNameKey);
                }
            }
            else
            {
                if (propertyErrors.Any())
                {
                    _errors[propertyNameKey] = propertyErrors;
                    RaiseErrorsChanged(propertyNameKey);
                }
            }
        }

        /// <summary>
        ///     Raises this object's ErrorsChangedChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has new errors.</param>
        protected virtual void RaiseErrorsChanged(string propertyName)
        {
            var handler = ErrorsChanged;
            if (handler != null)
            {
                handler(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        ///     Raises this object's ErrorsChangedChanged event.
        /// </summary>
        /// <typeparam name="T">The type of the property that has errors</typeparam>
        /// <param name="propertyExpresssion">A Lambda expression representing the property that has new errors.</param>
        protected virtual void RaiseErrorsChanged<T>(Expression<Func<T>> propertyExpresssion)
        {
            var propertyName = ExtractPropertyName(propertyExpresssion);
            RaiseErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Compares error collections
        /// </summary>
        /// <param name="propertyErrors">The property errors</param>
        /// <param name="currentPropertyErrors">The current</param>
        /// <returns>True if there are/aren't equal</returns>
        private static bool _AreErrorCollectionsEqual(IEnumerable<string> propertyErrors,
                                                     IEnumerable<string> currentPropertyErrors)
        {
            var equals = currentPropertyErrors.Zip(propertyErrors, (current, newError) => current == newError);
            return propertyErrors.Count() == currentPropertyErrors.Count() && equals.All(b => b);
        }

        public override string ToString()
        {
            return GetType().Name;
        }

        protected virtual void OnCommit(object argument)
        {
            ValidateAll();
            if (HasErrors) return;
            OnCommitted();
            Committed = true;
        }

        protected virtual bool CanCommit(object argument)
        {
            return !(HasErrors || Committed);
        }

        public override void OnImportsSatisfied()
        {
            base.OnImportsSatisfied();
        }

        private static void OnViewSample(string fileName)
        {
            if (fileName == null) return;

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Samples", fileName);

            if (string.IsNullOrEmpty(fileName) || !File.Exists(file))
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Sample file not found !", string.Format("MONAHRQ {0}", MonahrqContext.ApplicationVersion.SubStrBeforeLast(".")), MessageBoxButton.OK);
                return;
            }
            Process.Start(file);
        }

    }
}
