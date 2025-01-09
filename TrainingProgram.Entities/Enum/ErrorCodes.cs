namespace TrainingProgram.Entities.Enum
{
    public enum ErrorCodes
    {

        OperationCanceled = 0,
        UserNotFound = 1,
        UserAlreadyExists = 2,
        UserUnauthorizedAccess = 3,
        UserAlreadyExistsThisRole = 4,
        PasswordNotEqualsPasswordConfirm = 5,
        PasswordIsWrong = 6,
        RoleAlreadyExists = 7,
        RoleNotFound = 8,
        InternalServerError = 10,
        RegistrationFailed = 11,
        UserIsBanned = 12,
        AccessDenied = 21,
        CourseNotCreated = 31,
        CourseNotFounded = 32,
        CourseNotRetrieved = 33,
        CourseListNotRetrieved = 34,
        CourseNotUpdated = 35,
        CourseNotFound = 36,
        CourseNotDeleted = 37,
        LessonNotFound = 41,
        QuestionNotFound = 51,
        AnswerNotFound = 61,
    }
}
