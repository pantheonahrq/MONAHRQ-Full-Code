﻿using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.Composition;
using Monahrq.Sdk.Extensibility.Data.Migration;
using Monahrq.Sdk.Extensibility.Data.Migration.Schema;

namespace Monahrq.Wing.Discharge.Inpatient
{
   // [Export(MigrationContractNames.Target, typeof(ITargetMigration))]
    public partial class Migrations : DataMigrationImpl, ITargetMigration
    {
        public IEnumerable<string> TargetNames 
        { 
            get 
            { 
                return new string[] 
                {
                     "InpatientTarget"
                     ,  "TreatAndReleaseTarget"                    
                };
            } 
        }

        public override int Create()
        {
            //// Creating table Monahrq_Wing_Discharge_Inpatient_InpatientTarget
            //SchemaBuilder.CreateTable("Monahrq_Wing_Discharge_Inpatient_InpatientTarget", table => table
            //    .ContentPartVersionRecord()
            //    .Column("Key", DbType.String, column => column.WithLength(20))
            //    .Column("Age", DbType.Int32)
            //    .Column("AgeInDays", DbType.Int32)
            //    .Column("Race", DbType.String)
            //    .Column("Sex", DbType.String)
            //    .Column("PrimaryPayer", DbType.String)
            //    .Column("PatientStateCountyCode", DbType.String, column => column.WithLength(5))
            //    .Column("LocalHospitalID", DbType.String, column => column.WithLength(12))
            //    .Column("DischargeDisposition", DbType.String)
            //    .Column("AdmissionType", DbType.String)
            //    .Column("AdmissionSource", DbType.String)
            //    .Column("PointOfOrigin", DbType.String)
            //    .Column("LengthOfStay", DbType.Int32)
            //    .Column("DRGVersion", DbType.Int32)
            //    .Column("DiagnosisRelatedGroup", DbType.Int32)
            //    .Column("MSDRG", DbType.Int32)
            //    .Column("RATESDRG", DbType.Int32)
            //    .Column("DischargeDate", DbType.DateTime)
            //    .Column("DischargeYear", DbType.Int32)
            //    .Column("DischargeQuarter", DbType.Int32)
            //    .Column("MajorDiagnosticCategory", DbType.Int32)
            //    .Column("DaysOnMechVentilator", DbType.Int32)
            //    .Column("BirthWeightGrams", DbType.Int32)
            //    .Column("TotalCharge", DbType.Int32)
            //    .Column("ICDVER", DbType.Int32)
            //    .Column("PrincipalDiagnosis", DbType.String)
            //    .Column("DiagnosisCode2", DbType.String)
            //    .Column("DiagnosisCode3", DbType.String)
            //    .Column("DiagnosisCode4", DbType.String)
            //    .Column("DiagnosisCode5", DbType.String)
            //    .Column("DiagnosisCode6", DbType.String)
            //    .Column("DiagnosisCode7", DbType.String)
            //    .Column("DiagnosisCode8", DbType.String)
            //    .Column("DiagnosisCode9", DbType.String)
            //    .Column("DiagnosisCode10", DbType.String)
            //    .Column("DiagnosisCode11", DbType.String)
            //    .Column("DiagnosisCode12", DbType.String)
            //    .Column("DiagnosisCode13", DbType.String)
            //    .Column("DiagnosisCode14", DbType.String)
            //    .Column("DiagnosisCode15", DbType.String)
            //    .Column("DiagnosisCode16", DbType.String)
            //    .Column("DiagnosisCode17", DbType.String)
            //    .Column("DiagnosisCode18", DbType.String)
            //    .Column("DiagnosisCode19", DbType.String)
            //    .Column("DiagnosisCode20", DbType.String)
            //    .Column("DiagnosisCode21", DbType.String)
            //    .Column("DiagnosisCode22", DbType.String)
            //    .Column("DiagnosisCode23", DbType.String)
            //    .Column("DiagnosisCode24", DbType.String)
            //    .Column("DiagnosisCode25", DbType.String)
            //    .Column("DiagnosisCode26", DbType.String)
            //    .Column("DiagnosisCode27", DbType.String)
            //    .Column("DiagnosisCode28", DbType.String)
            //    .Column("DiagnosisCode29", DbType.String)
            //    .Column("DiagnosisCode30", DbType.String)
            //    .Column("DiagnosisCode31", DbType.String)
            //    .Column("DiagnosisCode32", DbType.String)
            //    .Column("DiagnosisCode33", DbType.String)
            //    .Column("DiagnosisCode34", DbType.String)
            //    .Column("DiagnosisCode35", DbType.String)
            //    .Column("PrincipalProcedure", DbType.String)
            //    .Column("ProcedureCode2", DbType.String)
            //    .Column("ProcedureCode3", DbType.String)
            //    .Column("ProcedureCode4", DbType.String)
            //    .Column("ProcedureCode5", DbType.String)
            //    .Column("ProcedureCode6", DbType.String)
            //    .Column("ProcedureCode7", DbType.String)
            //    .Column("ProcedureCode8", DbType.String)
            //    .Column("ProcedureCode9", DbType.String)
            //    .Column("ProcedureCode10", DbType.String)
            //    .Column("ProcedureCode11", DbType.String)
            //    .Column("ProcedureCode12", DbType.String)
            //    .Column("ProcedureCode13", DbType.String)
            //    .Column("ProcedureCode14", DbType.String)
            //    .Column("ProcedureCode15", DbType.String)
            //    .Column("ProcedureCode16", DbType.String)
            //    .Column("ProcedureCode17", DbType.String)
            //    .Column("ProcedureCode18", DbType.String)
            //    .Column("ProcedureCode19", DbType.String)
            //    .Column("ProcedureCode20", DbType.String)
            //    .Column("ProcedureCode21", DbType.String)
            //    .Column("ProcedureCode22", DbType.String)
            //    .Column("ProcedureCode23", DbType.String)
            //    .Column("ProcedureCode24", DbType.String)
            //    .Column("ProcedureCode25", DbType.String)
            //    .Column("ProcedureCode26", DbType.String)
            //    .Column("ProcedureCode27", DbType.String)
            //    .Column("ProcedureCode28", DbType.String)
            //    .Column("ProcedureCode29", DbType.String)
            //    .Column("ProcedureCode30", DbType.String)
            //    .Column("DaysToProcedure1", DbType.Int32)
            //    .Column("DaysToProcedure2", DbType.Int32)
            //    .Column("DaysToProcedure3", DbType.Int32)
            //    .Column("DaysToProcedure4", DbType.Int32)
            //    .Column("DaysToProcedure5", DbType.Int32)
            //    .Column("DaysToProcedure6", DbType.Int32)
            //    .Column("DaysToProcedure7", DbType.Int32)
            //    .Column("DaysToProcedure8", DbType.Int32)
            //    .Column("DaysToProcedure9", DbType.Int32)
            //    .Column("DaysToProcedure10", DbType.Int32)
            //    .Column("DaysToProcedure11", DbType.Int32)
            //    .Column("DaysToProcedure12", DbType.Int32)
            //    .Column("DaysToProcedure13", DbType.Int32)
            //    .Column("DaysToProcedure14", DbType.Int32)
            //    .Column("DaysToProcedure15", DbType.Int32)
            //    .Column("DaysToProcedure16", DbType.Int32)
            //    .Column("DaysToProcedure17", DbType.Int32)
            //    .Column("DaysToProcedure18", DbType.Int32)
            //    .Column("DaysToProcedure19", DbType.Int32)
            //    .Column("DaysToProcedure20", DbType.Int32)
            //    .Column("DaysToProcedure21", DbType.Int32)
            //    .Column("DaysToProcedure22", DbType.Int32)
            //    .Column("DaysToProcedure23", DbType.Int32)
            //    .Column("DaysToProcedure24", DbType.Int32)
            //    .Column("DaysToProcedure25", DbType.Int32)
            //    .Column("DaysToProcedure26", DbType.Int32)
            //    .Column("DaysToProcedure27", DbType.Int32)
            //    .Column("DaysToProcedure28", DbType.Int32)
            //    .Column("DaysToProcedure29", DbType.Int32)
            //    .Column("DaysToProcedure30", DbType.Int32)
            //    .Column("CustomStratifier1", DbType.String, column => column.WithLength(20))
            //    .Column("CustomStratifier2", DbType.String, column => column.WithLength(20))
            //    .Column("CustomStratifier3", DbType.String, column => column.WithLength(20))
            //    .Column("PresentOnAdmission1", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission2", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission3", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission4", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission5", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission6", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission7", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission8", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission9", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission10", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission11", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission12", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission13", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission14", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission15", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission16", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission17", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission18", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission19", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission20", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission21", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission22", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission23", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission24", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission25", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission26", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission27", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission28", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission29", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission30", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission31", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission32", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission33", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission34", DbType.String, column => column.WithLength(1))
            //    .Column("PresentOnAdmission35", DbType.String, column => column.WithLength(1))
            //    .Column("PatientID", DbType.String, column => column.WithLength(20))
            //    .Column("BirthDate", DbType.DateTime)
            //    .Column("AdmissionDate", DbType.DateTime)
            //    .Column("DRGImport", DbType.Int32)
            //    .Column("MDCImport", DbType.Int32)
            //    .Column("EDServices", DbType.String)
            //);

            //// Creating table Monahrq_Wing_Discharge_TreatAndRelease_TreatAndReleaseTarget
            //SchemaBuilder.CreateTable("Monahrq_Wing_Discharge_TreatAndRelease_TreatAndReleaseTarget", table => table
            //    .ContentPartVersionRecord()
            //    .Column("Key", DbType.String, column => column.WithLength(20))
            //    .Column("Age", DbType.Int32)
            //    .Column("Race", DbType.String)
            //    .Column("Sex", DbType.String)
            //    .Column("PrimaryPayer", DbType.String)
            //    .Column("PatientStateCountyCode", DbType.String, column => column.WithLength(5))
            //    .Column("LocalHospitalID", DbType.String, column => column.WithLength(12))
            //    .Column("DischargeDisposition", DbType.String)
            //    .Column("DischargeYear", DbType.Int32)
            //    .Column("PrimaryDiagnosis", DbType.String)
            //    .Column("DiagnosisCode2", DbType.String)
            //    .Column("DiagnosisCode3", DbType.String)
            //    .Column("DiagnosisCode4", DbType.String)
            //    .Column("DiagnosisCode5", DbType.String)
            //    .Column("DiagnosisCode6", DbType.String)
            //    .Column("DiagnosisCode7", DbType.String)
            //    .Column("DiagnosisCode8", DbType.String)
            //    .Column("DiagnosisCode9", DbType.String)
            //    .Column("DiagnosisCode10", DbType.String)
            //    .Column("DiagnosisCode11", DbType.String)
            //    .Column("DiagnosisCode12", DbType.String)
            //    .Column("DiagnosisCode13", DbType.String)
            //    .Column("DiagnosisCode14", DbType.String)
            //    .Column("DiagnosisCode15", DbType.String)
            //    .Column("DiagnosisCode16", DbType.String)
            //    .Column("DiagnosisCode17", DbType.String)
            //    .Column("DiagnosisCode18", DbType.String)
            //    .Column("DiagnosisCode19", DbType.String)
            //    .Column("DiagnosisCode20", DbType.String)
            //    .Column("HospitalTraumaLevel", DbType.String)
            //    .Column("NumberofDiagnoses", DbType.Int32)
            //);



            return 1;
        }
    }
}