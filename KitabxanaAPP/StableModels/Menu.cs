namespace KitabxanaAPP.StableModels
{
    public enum MainMenu : byte
    {

        Author = 1,
        Book,
        SaveAndExit

    }

    public enum AuthorSubMenu : byte
    {
        CreateAuthor = 1,
        DeleteAuthor,
        EditAuthor,
        GetAllAuthor,
        GetAuthorById,
        GoHome
    }

    public enum BookSubMenu : byte
    {
        CreateBook = 1,
        DeleteBook,
        EditBook,
        GetAllBook,
        GetBookById,
        GoHome

    }
}
