namespace Game
{
    public static class GameStateController
    {
        public static void UpdateGameState(IGameState _)
        {
            switch (GameController.GameState)
            {
                case GameState.INIT:
                    _.OnGameInit();
                    break;
                case GameState.BEFORE_START:
                    _.OnGameBeforeStart();
                    break;
                case GameState.START:
                    _.OnGameStart();
                    break;
                case GameState.CLEARANCE:
                    _.OnGameClearance();
                    break;
                case GameState.PAUSE:
                    _.OnGamePause();
                    break;
                case GameState.FINISH:
                    _.OnGameFinish();
                    break;
                case GameState.NULL:
                default:
                    _.OnGameIddle();
                    break;
            }
        }
    }
}
