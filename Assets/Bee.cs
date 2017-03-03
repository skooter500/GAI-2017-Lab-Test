
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public Bee bee;
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    
}

class WanderState : State
{
    public override void Enter()
    {
        FlowerSpawner flowers = GameObject.FindObjectOfType<FlowerSpawner>();
        
        bee.arriveEnabled = true;
        bee.arriveTarget = new Vector3(
            Random.Range(-flowers.radius, flowers.radius)
            , 0.5f
            , Random.Range(-flowers.radius, flowers.radius)
            );
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        GameObject[] flowersInBloom = GameObject.FindGameObjectsWithTag("flower");
        if (Vector3.Distance(bee.transform.position, bee.arriveTarget) < 5)
        {
            bee.ChangeState(new WanderState());
            return;
        }

        foreach (GameObject flower in flowersInBloom)
        {                   
            if (flower != null && Vector3.Distance(flower.transform.position, bee.transform.position) < 5)
            {
                bee.ChangeState(new GoToResource(flower.gameObject));
            }
        }

    }
}

class GoToResource: State
{
    public Flower flower;
    public GoToResource(GameObject flower)
    {
        this.flower = flower.GetComponent<Flower>();
    }
    public override void Enter()
    {
        bee.arriveEnabled = true;
        Vector3 pos = flower.transform.position;
        pos.y = 0.5f;
        bee.arriveTarget = pos;
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (flower == null || flower.gameObject == null)
        {
            bee.ChangeState(new WanderState());
            return;
        }
        if (Vector3.Distance(bee.transform.position, flower.transform.position) < 2f)
        {
            bee.ChangeState(new ConsumeResource(flower));
        }        
    }
}

class ConsumeResource:State
{
    Flower flower;
    public ConsumeResource(Flower flower)
    {
        this.flower = flower;
    }

    public override void Enter()
    {
        bee.arriveEnabled = false;
        //e.velocity = Vector3.zero;
    }

    public override void Update()
    {
        if (flower == null)
        {
            bee.ChangeState(new ReturnToHive());
            return;
        }
        // Is this better than checking the polen.y
        if (flower.transform.localScale.y <= 0)
        {
            GameObject.Destroy(flower.gameObject);
            bee.ChangeState(new ReturnToHive());
            return;
        }
        flower.polen -= Time.deltaTime;
        bee.polen += Time.deltaTime;

    }
}

class ReturnToHive : State
{
    GameObject hive;
    public override void Enter()
    {
        bee.arriveEnabled = true;
        hive = GameObject.FindGameObjectWithTag("hive");
        bee.arriveTarget = hive.transform.position;
    }

    public override void Update()
    {
        if (Vector3.Distance(bee.transform.position, hive.transform.position) < 1.0f)
        {
            hive.GetComponent<Hive>().polen += bee.polen;
            bee.polen = 0;
            bee.ChangeState(new WanderState());
        }
    }
}

public class Bee : MonoBehaviour {
    public Vector3 force;
    public Vector3 acceleration;
    public Vector3 velocity;

    public float maxSpeed = 5;
    public float mass = 1;

    public bool arriveEnabled = false;
    public Vector3 arriveTarget;
    public float slowingDistance = 20;

    
    public float polen = 0;

    State state;

    public Vector3 Arrive(Vector3 target, float slowing)
    {
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;

        float clamped = maxSpeed * (dist / slowing);
        float ramped = Mathf.Min(clamped, maxSpeed);

        Vector3 desired = (toTarget / dist) * ramped;
        return desired - velocity;
    }

    public void ChangeState(State newState)
    {
        if (state != null)
        {
            Debug.Log("Exiting: " + state.GetType().Name);
            state.Exit();
        }
        state = newState;
        state.bee = this;
        Debug.Log("entering: " + state.GetType().Name);
        state.Enter();
    }

    // Use this for initialization
    void Start () {
        ChangeState(new WanderState());

        StartCoroutine(Think());
	}

    System.Collections.IEnumerator Think()
    {
        while (true)
        {
            state.Update();
            yield return new WaitForSeconds(0.2f);
        }
    }
	// Update is called once per frame
	void Update () {

        state.Update();

        if (arriveEnabled)
        {
            force += Arrive(arriveTarget, slowingDistance);
        }
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > 0.01f)
        {
            transform.forward = velocity;
        }

        velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime);

        force = Vector3.zero;
	}
}
