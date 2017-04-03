namespace ConUI
{
    public static class ArrayExt
    {
        public static void Populate<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }
        public static void Populate<T>(this T[,] array, T value)
        {
            int xLength = array.GetLength(0);
            int yLength = array.GetLength(1);

            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    array[x, y] = value;
                }
            }
        }
    }
}
