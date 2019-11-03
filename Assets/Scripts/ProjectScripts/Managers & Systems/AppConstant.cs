using UnityEngine;
using System.Collections;


public class AppPaths
{
	public static readonly string  	PERSISTENT_DATA      = Application.persistentDataPath;
	public static readonly string	PATH_RESOURCE_SFX    = "Audio/FX/";
    public static readonly string	PATH_RESOURCE_MUSIC  = "Audio/Music/";
    public static readonly string   PATH_RESOURCE_CHARACTERS = "ScriptableObjects/Characters/";
}

public class AppScenes
{
    public static readonly string MAIN_SCENE = "Introducir escenas";
}

public class AppPlayerPrefKeys
{
	public static readonly string	MUSIC_VOLUME = "MusicVolume";
	public static readonly string	SFX_VOLUME   = "SfxVolume";
}

public class AppSounds
{
	public static readonly string	MAIN_TITLE_MUSIC = "MainTitle";
	public static readonly string	GAME_MUSIC       = "GameMusic";
    public static readonly string	BUTTON_SFX       = "ButtonEffect";
    public static readonly string   DOOR_SFX         = "Door_Close";
}
public class AppSortingLayers
{
    public static readonly string CHARACTER_NORMAL = "Characters";
    public static readonly string CHARACTER_OVER = "CharactersOverPlayer";
    public static readonly string CHARACTER_BEHIND_OBJECTS = "CharactersBehindObjects";
}



