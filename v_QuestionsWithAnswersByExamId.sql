CREATE OR ALTER VIEW dbo.v_ExamQuestionsWithAnswers
AS 
(
SELECT 
EQ.ExamId,
Q.Id AS QuestionId,
Q.QuestionBody,
A.Id AS AnswerId,
A.AnswerBody
FROM dbo.Questions Q
INNER JOIN dbo.ExamsQuestions EQ ON Q.Id = EQ.QuestionId
INNER JOIN dbo.Answers A ON A.QuestionId = Q.Id
);
