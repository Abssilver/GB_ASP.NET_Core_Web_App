namespace CrossTask
{
    internal static class MoveNodeSortComparison
    {
        public static int CompareMoveNodesByPosition(MoveNode x, MoveNode y)
        {
            if (x is null)
            {
                if (y is null)
                {
                    return 0;
                }

                return -1;
            }

            if (y is null)
            {
                return 1;
            }

            int columnCompare = x.Position.Column.CompareTo(y.Position.Column);

            if (columnCompare != 0)
            {
                return columnCompare;
            }

            return x.Position.Row.CompareTo(y.Position.Row);
        }
    }
}