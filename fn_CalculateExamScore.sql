CREATE OR ALTER FUNCTION dbo.fn_CalculateExamScore(@ExamId INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @TotalSuccessPercentage FLOAT;

    WITH SuccessCTE AS (
        SELECT 
            CT.TopicId,
            (COUNT(Q.Id) * CT.TopicPercentage) / NULLIF(CT.QuestionCount, 0) AS SuccessPercentageByTopic
        FROM 
            dbo.ExamsQuestions EQ
            INNER JOIN dbo.Questions Q ON Q.Id = EQ.QuestionId
            JOIN dbo.Answers A ON A.Id = EQ.AnswerId
            INNER JOIN dbo.CertificateTopics CT ON CT.TopicId = Q.TopicId 
        WHERE 
            EQ.ExamId = @ExamId AND A.IsRight = 1
        GROUP BY 
            CT.TopicId, 
            CT.QuestionCount, 
            CT.TopicPercentage
    )

    SELECT @TotalSuccessPercentage = SUM(SuccessPercentageByTopic)
    FROM SuccessCTE;

    RETURN @TotalSuccessPercentage;
END;


--SELECT 
--CT.TopicId,
--CT.QuestionCount,
--COUNT(Q.Id) AS NumberOfRightQuestions,
--CT.TopicPercentage,
--(COUNT(Q.Id) * CT.TopicPercentage * 100) / NULLIF(CT.QuestionCount, 0) AS SuccessPercentageByTopic,
--SUM((COUNT(Q.Id) * CT.TopicPercentage * 100) / NULLIF(CT.QuestionCount, 0)) OVER () AS TotalSuccessPercentage
--From dbo.ExamsQuestions EQ
--INNER JOIN dbo.Questions Q ON Q.Id = EQ.QuestionId
--JOIN dbo.Answers A ON A.Id = EQ.AnswerId
--INNER JOIN dbo.CertificateTopics CT ON CT.TopicId = Q.TopicId 
--WHERE EQ.ExamId = 5 AND A.IsRight = 1
--GROUP BY 
--CT.TopicId, 
--CT.QuestionCount, 
--CT.TopicPercentage;

