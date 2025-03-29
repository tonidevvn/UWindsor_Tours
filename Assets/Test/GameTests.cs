using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameTests
{
    private GameObject player;
    private GameObject npc;
    private GameObject interactable;
    
    [SetUp]
    public void Setup()
    {
        player = new GameObject("Player");
        player.AddComponent<CharacterController>();

        npc = new GameObject("NPC");
        interactable = new GameObject("Interactable");
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(player);
        Object.Destroy(npc);
        Object.Destroy(interactable);
    }

    [Test]
    public void PlayerExistsInScene()
    {
        Assert.IsNotNull(player);
    }
    
    [UnityTest]
    public IEnumerator PlayerCanMove()
    {
        Vector3 startPosition = player.transform.position;
        player.transform.position += Vector3.forward;
        yield return null;
        Assert.AreNotEqual(startPosition, player.transform.position);
    }
    
    [UnityTest]
    public IEnumerator NPCRespondsToPlayer()
    {
        npc.AddComponent<NPCBehavior>();
        yield return new WaitForSeconds(1);
        Assert.IsTrue(npc.GetComponent<NPCBehavior>().hasInteracted);
    }
    
    [UnityTest]
    public IEnumerator SceneLoadsCorrectly()
    {
        yield return new WaitForSeconds(1);
        Assert.AreEqual("MainScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    [Test]
    public void CollisionDetectionWorks()
    {
        player.AddComponent<BoxCollider>();
        npc.AddComponent<BoxCollider>();
        player.AddComponent<Rigidbody>();
        npc.AddComponent<Rigidbody>();
        
        bool collisionOccurred = false;
        player.GetComponent<Collider>().enabled = true;
        npc.GetComponent<Collider>().enabled = true;

        Physics.Simulate(Time.fixedDeltaTime);
        collisionOccurred = player.GetComponent<Collider>().bounds.Intersects(npc.GetComponent<Collider>().bounds);

        Assert.IsTrue(collisionOccurred);
    }
    
    [UnityTest]
    public IEnumerator PlayerInteractsWithObject()
    {
        interactable.AddComponent<InteractableObject>();
        yield return new WaitForSeconds(1);
        Assert.IsTrue(interactable.GetComponent<InteractableObject>().hasBeenInteracted);
    }
    
    [Test]
    public void NPCPathfindingWorks()
    {
        npc.AddComponent<UnityEngine.AI.NavMeshAgent>();
        Vector3 targetPosition = new Vector3(5, 0, 5);
        npc.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(targetPosition);
        Assert.AreEqual(targetPosition, npc.GetComponent<UnityEngine.AI.NavMeshAgent>().destination);
    }
    
    [UnityTest]
    public IEnumerator PlayerHealthDecreasesOnDamage()
    {
        PlayerHealth playerHealth = player.AddComponent<PlayerHealth>();
        playerHealth.TakeDamage(10);
        yield return null;
        Assert.AreEqual(90, playerHealth.currentHealth);
    }
    
    [UnityTest]
    public IEnumerator GameOverTriggersOnDeath()
    {
        PlayerHealth playerHealth = player.AddComponent<PlayerHealth>();
        playerHealth.TakeDamage(100);
        yield return null;
        Assert.IsTrue(playerHealth.isGameOver);
    }
    
    [Test]
    public void UIElementsExist()
    {
        GameObject uiCanvas = new GameObject("UICanvas");
        GameObject healthBar = new GameObject("HealthBar");
        healthBar.transform.SetParent(uiCanvas.transform);
        Assert.IsNotNull(healthBar);
    }
}
