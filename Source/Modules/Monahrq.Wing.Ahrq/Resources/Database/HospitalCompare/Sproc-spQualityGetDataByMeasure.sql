/*
 *      Name:           spQualityGetDataByMeasure
 *      Version:        1.0
 *      Last Updated:   4/9/14
 *      Used In:        QualityReportGenerator.cs
 *      Description:    Used to get quality report data for all hospitals for one particular measure.
 */

IF EXISTS (
	SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE SPECIFIC_SCHEMA = N'dbo' AND SPECIFIC_NAME = N'spQualityGetDataByMeasure' 
)
	DROP PROCEDURE dbo.spQualityGetDataByMeasure
GO

CREATE PROCEDURE [dbo].[spQualityGetDataByMeasure]
	@ReportID uniqueidentifier, @MeasureID int
AS
BEGIN
    SET NOCOUNT ON;

            SELECT DISTINCT MeasureID
                  ,HospitalID
                  ,CountyID
                  ,RegionID
                  ,ZipCode
                  ,HospitalType
                  ,RateAndCI
                  ,NatRating
                  ,NatFilled
                  ,PeerRating
                  ,PeerFilled
                  ,Col1
                  ,Col2
                  ,Col3
                  ,Col4
                  ,Col5
                  ,Col6
                  ,Col7
                  ,Col8
                  ,Col9
                  ,Col10
            FROM dbo.Temp_Quality
            WHERE MeasureID = @MeasureID AND ReportID = @ReportID
        
END
