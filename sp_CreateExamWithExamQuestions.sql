CREATE OR ALTER PROCEDURE [dbo].[sp_CreateExamAndQuestions]
    @UserId NVARCHAR(255),
    @CertificateId INT
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Your SQL statements (inserts, updates, etc.) go here If any statement fails, an error will be caught in the TRY block
		-- Variables
		DECLARE @ExamId INT;
		DECLARE @ExamDurationInMinutes INT;
		SELECT @ExamDurationInMinutes = C.TestDurationInMinutes FROM Certificates C WHERE C.Id = @CertificateId;
		IF CURSOR_STATUS('global', 'topic_cursor') >= 0
		BEGIN
			-- If it exists, close and deallocate it
			CLOSE topic_cursor;
			DEALLOCATE topic_cursor;
		END
 
		-- Add Exam row
		INSERT INTO Exams(CertificateId, UserId, ExamStartTime, ExamEndTime, CreatedAt, IsDeleted)
		VALUES (@CertificateId, @UserId, GETDATE(), DATEADD(MINUTE, @ExamDurationInMinutes, GETDATE()) ,GETDATE(), 0);
 
		SET @ExamId = SCOPE_IDENTITY();
 
		-- Get All TopicIds and TopicQuestionCount by certificate id
		DECLARE @TopicId INT;
		DECLARE @QuestionCount INT;

		DECLARE topic_cursor CURSOR FOR
		SELECT TopicId, QuestionCount
		FROM CertificateTopics
		WHERE CertificateId = @CertificateId;
 
		OPEN topic_cursor;
		FETCH NEXT FROM topic_cursor INTO @TopicId, @QuestionCount;
 
		-- Loop through topic_cursor and insert random questions with specified @QuestionCount
		WHILE @@FETCH_STATUS = 0
		BEGIN
			INSERT INTO ExamsQuestions (ExamId, QuestionId, CreatedAt, IsDeleted)
			SELECT TOP (@QuestionCount) @ExamId, Q.Id, GETDATE(), 0
			FROM Topics T
			JOIN Questions Q ON T.Id = Q.TopicId
			WHERE T.Id = @TopicId
			ORDER BY NEWID();  -- Use NEWID()  to get unique identifier
 
			FETCH NEXT FROM topic_cursor INTO @TopicId, @QuestionCount;
		END;
 
		CLOSE topic_cursor;
		DEALLOCATE topic_cursor;

        -- If everything is successful, commit the transaction
        COMMIT TRANSACTION;

		-- If Transaction successful get exam questions with answers
		--SELECT 
		--E.Id,
		--E.CertificateId,
		--E.UserId,
		--E.ExamStartTime,
		--E.ExamEndTime,
		--E.ExamCompletedTime,
		--E.IsPassed,
		--E.ExamScore,
		--C.CertificateName,
		--C.TestDurationInMinutes,
		--C.PassScore,
		--E.CreatedAt
		--FROM Exams E
		--INNER JOIN Certificates C ON C.Id = E.CertificateId
		--WHERE E.Id = @ExamId;
		SELECT * FROM Exams E WHERE E.Id = @ExamId
		--SELECT * FROM Exams E INNER JOIN Certificates C ON E.CertificateId = C.Id WHERE E.Id = @ExamId
		--SELECT * FROM dbo.QuestionsWithAnswersByExamId E WHERE E.ExamId = @ExamId; 

    END TRY
    BEGIN CATCH
        -- If an error occurs, roll back the transaction
        ROLLBACK TRANSACTION;
		THROW;
        -- Optionally: You can handle or log the error here
    END CATCH;
END;