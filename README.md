# Bitboard
A bitboard is a specialized bit array data structure commonly used in computer systems that play board games, where each bit corresponds to a game board space or piece. This allows parallel bitwise operations to set or query the game state, or determine moves or plays in the game.

## Features
### State Control Using Bitwise Operations

```csharp
public long SetCellState(long bitboard, int row, int col)
{
    long newBit = 1L << (row * 8 + col);
    return (bitboard |= newBit);
}

```

```csharp
public bool GetCellState(long bitboard, int row, int col)
{
    long mask = 1L << (row * 8 + col);
    return ((bitboard & mask) != 0);
}
```

![Unity_WJKutTUlsd](https://github.com/oguzhandelibas/Bitboard/assets/64430254/c4870a19-0c35-4d1f-a36d-730ee87e3a89)
