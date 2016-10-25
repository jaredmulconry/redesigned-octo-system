using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CheckpointManager : MonoBehaviour
{
    public string CheckpointFileName = "Checkpoint.dat";
    string checkpointDirectory;
    string tempDirectory;

    public GameObject[] ObjectPrefabs;

    /// <summary>
    /// Invoked prior to any saving occurring.
    /// </summary>
    public delegate void SaveCallback();
    /// <summary>
    /// Invoked after loading has occurred.
    /// </summary>
    public delegate void LoadCallback();

    SaveCallback preSaveCallback;
    LoadCallback postLoadCallback;

    public void AddPreSaveCallback(SaveCallback callee)
    {
        preSaveCallback += callee;
    }
    public void RemovePreSaveCallback(SaveCallback callee)
    {
        preSaveCallback -= callee;
    }
    public void AddPostLoadCallback(LoadCallback callee)
    {
        postLoadCallback += callee;
    }
    public void RemovePosLoadCallback(LoadCallback callee)
    {
        postLoadCallback -= callee;
    }

    void Awake()
    {
        checkpointDirectory = Application.dataPath;
        tempDirectory = Application.temporaryCachePath;

        preSaveCallback = delegate { };
        postLoadCallback = delegate { };

        Debug.Log("Checkpoint Directory: " + checkpointDirectory);
        Debug.Log("Temp Directory: " + tempDirectory);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Save();
        }
        if(Input.GetKeyDown(KeyCode.F11))
        {
            Load();
        }
    }

    public void Save()
    {
        preSaveCallback();

        using (StreamWriter fileWriter = new StreamWriter(
                                    Path.Combine(tempDirectory,
                                                 CheckpointFileName)))
        {
            //Binary Writer is necessary to tell the difference between
            //two things we have saved. e.g. Saving two numbers one after
            //the other. Telling when one number ends and the other begins.
            BinaryWriter bWriter = new BinaryWriter(fileWriter.BaseStream);
            CheckpointID[] objectsToBeSaved =
                FindObjectsOfType<CheckpointID>();

            bWriter.Write(objectsToBeSaved.Length);

            foreach(CheckpointID id in objectsToBeSaved)
            {
                bWriter.Write(id.PrefabID);
                bWriter.Write(id.gameObject.name);
                Vector3 position = id.transform.position;
                bWriter.Write(position.x);
                bWriter.Write(position.y);
                bWriter.Write(position.z);
                Quaternion rotation = id.transform.rotation;
                bWriter.Write(rotation.x);
                bWriter.Write(rotation.y);
                bWriter.Write(rotation.z);
                bWriter.Write(rotation.w);
            }
        }

        File.Copy(Path.Combine(tempDirectory, CheckpointFileName),
                    Path.Combine(checkpointDirectory, CheckpointFileName),
                    true);
        File.Delete(Path.Combine(tempDirectory, CheckpointFileName));
    }
    public void Load()
    {
        CheckpointID[] objectsToBeDestroyed =
                FindObjectsOfType<CheckpointID>();

        foreach(CheckpointID id in objectsToBeDestroyed)
        {
            Destroy(id.gameObject);
        }

        if(!File.Exists(Path.Combine(checkpointDirectory,
                                    CheckpointFileName)))
        {
            return;
        }

        using (StreamReader fileReader = new StreamReader(
               Path.Combine(checkpointDirectory, 
                            CheckpointFileName)))
        {
            BinaryReader bReader = new BinaryReader(fileReader.BaseStream);

            int numberOfObjects = bReader.ReadInt32();

            for(int i = 0; i < numberOfObjects; ++i)
            {
                int prefabID = bReader.ReadInt32();
                string objectName = bReader.ReadString();
                Vector3 objectPosition = new Vector3(bReader.ReadSingle(),
                                                     bReader.ReadSingle(),
                                                     bReader.ReadSingle());
                Quaternion objectRotation = new Quaternion(bReader.ReadSingle(),
                                                           bReader.ReadSingle(),
                                                           bReader.ReadSingle(),
                                                           bReader.ReadSingle());

                if (prefabID < 0 || prefabID >= ObjectPrefabs.Length)
                {
                    Debug.LogWarning("Object was skipped. Name: " + objectName
                        + " ID: " + prefabID.ToString());
                    continue;
                }

                Instantiate(ObjectPrefabs[prefabID], objectPosition,
                            objectRotation);
            }
        }

        StartCoroutine(DelayedPostLoad());
    }

    IEnumerator DelayedPostLoad()
    {
        yield return new WaitForEndOfFrame();

        postLoadCallback();
    }
}
