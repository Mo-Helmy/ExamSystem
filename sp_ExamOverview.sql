SELECT * FROM [ExamSystem_MyCopy_V3].[dbo].[Exams];
GO;

SELECT * FROM [ExamSystem_MyCopy_V3].[dbo].[ExamsQuestions];
Go;


EXEC [dbo].[CreateExamAndQuestions] 'user1', 1;

