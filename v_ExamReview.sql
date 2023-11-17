CREATE OR ALTER VIEW dbo.v_ExamReview
AS
(
SELECT 
	EQ.ExamId,
	EQ.QuestionId,
	Q.QuestionBody,
	EQ.AnswerId AS AnswerId,
	A.AnswerBody,
	A.IsRight
	FROM dbo.ExamsQuestions EQ
	INNER JOIN dbo.Questions Q ON Q.Id = EQ.QuestionId
	LEFT JOIN dbo.Answers A ON EQ.AnswerId = A.Id
);
