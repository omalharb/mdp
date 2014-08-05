using UnityEngine;
using System.Collections;

public class Nutrient : MDPEntity 
{
    public Color BodyColor
    {
        get { return m_BodyColor; }
        set
        {
            m_BodyColor = value;
            renderer.materials[0].color = value;
        }
    }
    private Color m_BodyColor;

    private Color m_TrueColor;
    private Color m_TargetColor;

    public IntestineGameManager intestineGameManager;
	
    public bool IsTargetted;
	
	public bool isDead = false;
	private float elapsedTime = 0;

	protected GameObject m_Parent;
	
	private NutrientManager nutrientManager;

	public FoodBlobEffectParticles effectParticleParent;
	private FoodBlobEffectParticles foodBlobEffectParticles;

	// Use this for initialization
	void Start () 
	{
		nutrientManager = FindObjectOfType(typeof(NutrientManager)) as NutrientManager;
        Collider = new CircleCollider(this);
        IsTargetted = false;

        if (gameObject.transform.parent)
		    m_Parent = gameObject.transform.parent.gameObject;
	}
	
	void Update()
	{
		if(isDead && elapsedTime < 1)
		{
			elapsedTime += Time.deltaTime / 3;
			nutrientManager.ChangeColor(this, Color.Lerp(m_TrueColor, m_TargetColor, elapsedTime));
		}
	}
	
	public void OnBulletCollision ()
	{
		Absorb();
        if (m_BodyColor == Color.green)		// if the color of the nutrient was green we need to turn it white
        {
			nutrientManager.ChangeColor(this, Color.white);
			((Behaviour)gameObject.GetComponent("Halo")).enabled = true;
        }
        else
        {
			((Behaviour)gameObject.GetComponent("Halo")).enabled = false;
			isDead = true;
            m_TargetColor = new Color(92f / 255f, 64f / 255f, 51f / 255f);
			IsTargetted = true;
        }
		
		m_TrueColor = m_BodyColor;

		intestineGameManager.OnNutrientHit();

		foodBlobEffectParticles = (FoodBlobEffectParticles)Instantiate (effectParticleParent);
		foodBlobEffectParticles.createParticles(m_BodyColor);
		foodBlobEffectParticles.transform.position = gameObject.transform.position;
	}
	
	virtual protected void Absorb()
	{
		FoodBlob blob = m_Parent.GetComponent<FoodBlob>();
		blob.TakeHit();

        IsTargetted = false;
	}
}
