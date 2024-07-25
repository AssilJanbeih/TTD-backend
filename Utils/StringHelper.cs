namespace Utils;

public static class StringHelper
{
    public const string DateTimeString = "dd/MM/yyyy h:mm tt";
    public static T[,] ToRectangularArray<T>(this T[][] source)
    {
        int rowCount = source.Length;
        int columnCount = source.Max(array => array.Length);
        T[,] result = new T[rowCount, columnCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < source[i].Length; j++)
            {
                result[i, j] = source[i][j];
            }
        }

        return result;
    }
}