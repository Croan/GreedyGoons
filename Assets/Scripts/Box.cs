using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }
public enum BoxType { damage, hold, collision , stand}
public enum BoxGender { pepee, vago }
public class Box : MonoBehaviour
{
    public GameObjectEvent onEnter;
    public GameObjectEvent onStay;
    public GameObjectEvent onExit;
    private Collider2D col;

    public List<GameObject> contained;
    public Dictionary<GameObject, float> startTimes;

    public GameObject owner;

    //public BoxType type;

    public BoxGender gender;
    public BoxType type;
    public bool on = true; // changing th is at runtime will botch it, just sAiyin
    public bool onStatusLocked = false;
    public List<GameObject> ignores;

    private void Awake()
    {
        ignores = new List<GameObject>();
        col = GetComponent<Collider2D>();
        contained = new List<GameObject>();
        startTimes = new Dictionary<GameObject, float>();
        owner = transform.root.gameObject; 
        //SetupOwnerAndHeight();
    }
    private void Start()
    {
        //SetupOwnerAndHeight();

        onEnter.AddListener(OnEnter);
        onStay.AddListener(OnStay);
        onExit.AddListener(OnExit);
    }

    /*
    private void SetupOwnerAndHeight()
    {
        //owner = transform.root.gameObject;
        Transform tmp = transform;
        HeightHandler tmpHet;
        while (tmp != null)
        {
            tmpHet = tmp.GetComponent<HeightHandler>();
            if (tmpHet)
            {
                owner = tmp.gameObject;
                het = tmpHet;
                return;
            }
            tmp = tmp.parent;
        }
        owner = transform.root.gameObject;
       // print("warning " + owner.name + " got no heeight thing , making its owner root. careful now buddy boy ");
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Collide(collision, onEnter);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        // print(collision.gameObject.name +  " is exiting " + owner.name);
        Collide(collision, onExit);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        Collide(collision, onStay);
    }
    protected virtual void OnEnter(GameObject other)
    {
        
        //print("onEnter " + other.name);
        AddContained(other);
        // print("contained has " + contained.Count);
    }
    protected virtual void OnStay(GameObject other)
    {
        //print("on staying for " + owner.name + " with target " + other.name);
        if (!contained.Find(x => x == other))
        {
            //print("other wasnt in the contained");
            onEnter.Invoke(other);
        }
    }
    protected virtual void OnExit(GameObject other)
    {
        //print("removeinig from contained it had " + contained.Count);
        RemoveContained(other);
        //print("now it has " + contained.Count);
    }
    public static void ManualOnExit(Box b1, Box b2)
    {
        if (b1.Contains(b2.owner) != null)
        {
            b1.onExit.Invoke(b2.owner);
        }
        if (b2.Contains(b1.owner) != null)
        {
            b2.onExit.Invoke(b1.owner);
        }
    }
    protected void Collide(Collider2D x, GameObjectEvent ev)
    {
        
        var b = x.GetComponent<Box>();

        if (b)
        {
            if (ignores.Contains(b.owner))
            {
                return;
            }

            if (ev == onEnter)
            {//if youre checking  enter collide, and its already contained, dont bother
                if (Contains(b.owner))
                {
                    return;
                }
            }
            if (ev == onExit)
            {// if youre checking exit and it doesnt ontain it already, dont gbother
                if (!Contains(b.owner))
                {
                    return;
                }
            }
            if (Box.CheckValid(this, b))
            {
                ev.Invoke(b.owner);
            }
        }
    }
    protected static bool CheckValid(Box b1, Box b2)
    {
        if (!b1 || !b2)
        {
           // print("the frick? u feeding me nul shit? im a growing box! who the frick do u think u are?");

            return false;
        }
        if (b1.owner == b2.owner)
        {
            //print("same owner" + b1.owner);
            return false;
        }
        if (b1.gender == b2.gender)
        {
            // print("thats gay");
            return false;
        }
        if (b1.type != b2.type)
        {
            // print("b1 is " + b1.type + ", and b2 is " + b2.type);
            return false;
        }

        if (!b1.on || !b2.on)
        {
            ManualOnExit(b1, b2);
            //print("not turned on");
            return false;
        }


        return true;
    }
    /*
    private static bool CheckHeight(Box b1, Box b2)
    {
        if (!b1.het || !b2.het)
        {// print("doing het becaseu one is mising a height component");
            return true;
        }
        if (b1.heightOffset + b2.heightOffset >= Mathf.Abs((b1.het.WorldHeight + b1.middleHeight) - (b2.middleHeight + b2.het.WorldHeight)))
        {
            return true;
        }
        return false;
    }
    */
    /// <summary>
    /// currenty can only handle turning on with at most 100 colliders inside *shrugs*
    /// </summary>
    /// <param name="boov"></param>
    public void SetActive(bool boov)
    {
        if (onStatusLocked) { return; }
       // print(name + " im this " + boov);
        Collider2D[] ret = new Collider2D[100];
        Physics2D.OverlapCollider(col, (new ContactFilter2D()).NoFilter(), ret);
        if (boov && !on)
        {
            // print("turning on the box on " + owner.name);         
            foreach (var item in ret)
            {
                if (item)
                {               
                    Collide(item, onEnter);
                }
            }
        }
        else if(!boov && on)
        {
            foreach (var item in ret)
            {
                if (item)
                {
                    Collide(item, onExit);
                }
            }
        }

        on = boov;
    }
    public Vector2 GetCenterWorldCoords()
    {
        Vector2 ret = transform.position;
        ret += col.offset;
        
            return ret;
    }
    public Vector2 GetCenterLocalCoords()
    {
        Vector2 ret = transform.localPosition;
        ret += col.offset;

        return ret;
    }
    /// <summary>
    /// this is for invoking on exit when exiting without unity phyics picking up (aka if height gets out of range while still colliding)
    /// </summary>
    /// <param name="b1"></param>
    /// <param name="b2"></param>
    public GameObject Contains(GameObject own)
    {
        return (contained.Find(x => x == own));
    }
    public float GetContainedTime(GameObject g)
    {
        float f;
        if (startTimes.TryGetValue(g, out f))
        {
            return Time.time - f;
        }
        return Mathf.NegativeInfinity;
    }
    public void AddContained(GameObject g)
    {
        contained.Add(g);
        startTimes.Add(g, Time.time);
    }
    public void RemoveContained(GameObject g)
    {

        if (startTimes.Remove(g))
        {
            // print("succesfuly removed " + g.name);
        }
        contained.Remove(g);
        float f;
        startTimes.TryGetValue(g, out f);
        // print("just cleared it for " + g.name + ", but it still has a time of " + f);
    }
    public T GetContained<T>()
    {
        var x = GetContaineds<T>();
        if(x.Count>0)
        {
            return x[0];
        }
        else
        {
            return default(T);
        }
    }
    public List<T> GetContaineds<T>()
    {
        GameObject tmp=null;

        //print("calling getcontaindds " + contained.Count);
        List<T> ret = new List<T>();
        foreach (var item in contained)
        {
            if (item)
            {
                if (item.GetComponent<T>() != null)
                {
                    ret.Add(item.GetComponent<T>());
                }
            }
        }
        if (tmp)
        {
            RemoveContained(tmp);
        }
        return ret;
    }
    public static Box FindMy(BoxType t, BoxGender g, GameObject gO)
    {
        var x = gO.GetComponentsInChildren<Box>();
        foreach (var item in x)
        {
            if (item.type == t && item.gender == g)
            {
                return item;
            }
        }
        print("warning, found no box " + t.ToString() + " , " + g.ToString() + ", for " + gO.name);
        return null;

    }
    public void Reset()
    {
        SetActive(false);
        SetActive(true);
    }
    public void Debug(GameObject g)
    {
        print("Doing a test: " + g.name + " just collided with " + owner.name + " for box " + name);
    }
    private void DebugDictionary()
    {
        print("debugging the dictionary for the box on " + owner.name);
        foreach (KeyValuePair<GameObject, float> item in startTimes)
        {
            print(item.Key.name + " and the start Time is " + item.Value);
        }
    }

    private void OnDrawGizmos()
    {if (col)
        {
            Gizmos.DrawSphere(GetCenterWorldCoords(), 2);
        }
    }
}

