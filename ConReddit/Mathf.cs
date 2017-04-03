namespace ConReddit
{
    public static class Mathf
    {
        public static int ClampMin(this int i, int min)
        {
            if (i < min)
                return min;
            return i;
        }
    }
}
