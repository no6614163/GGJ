public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
        Enter,
        Exit,
    }


}

public enum Missions
{
    Eliminate,
    Survival,
    DestroyObject,
}

public enum HitType
{
    Normal,
    Critical,
}

public enum AttackType
{
    Normal,
    Pause,
    Knockback,
}

public enum LanguageCode
{
    KO,
    EN,
    JA,
}

public enum UserAction
{
    None,
    Attack,
    Special,
    Dash,
    ChangeWeapon,
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
}

public enum SceneType
{
    StartScene,
    LobbyScene,
    GameScene,
    LoadingScene,
}