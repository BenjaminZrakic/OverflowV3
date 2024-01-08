public struct CurrentStamina 
{
    public float previous;
    public float current;
    public float percentage;

    public CurrentStamina(float previousStamina, float currentStamina, float percentage) : this()
    {
        this.previous = previousStamina;
        this.current = currentStamina;
        this.percentage = percentage;
    }
}
