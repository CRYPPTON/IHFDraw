using System;

public class Representation
{
    private string country;
    private string continent;
    private string ihfRang;
    private int groupPosition;
    private string group;
    private int selection;
    private int score;
    private int goal;
    public Representation(string dataCountry, string dataContinent, string dataIhfRang, int datagroupPosition, string dataGroup, int dataSelection, int dataScore, int dataGoal)
	{
		country = dataCountry;
		continent = dataContinent;
		ihfRang = dataIhfRang;
        groupPosition = datagroupPosition;
        group = dataGroup;
        groupPosition = datagroupPosition;
        group = dataGroup;
        selection = dataSelection;
        score = dataScore;
        goal = dataGoal;
    }

    public string Country
    {
        get { return country; }
        set { country = value; }
    }

    public string Continent
    {
        get { return continent; }
        set { continent = value; }
    }
    public string IhfRang
    {
        get { return ihfRang; }
        set { ihfRang = value; }
    }

    public int GroupPosition
    {
        get { return groupPosition; }
        set { groupPosition = value; }
    }

    public string Group
    {
        get { return group; }
        set { group = value; }
    }

    public int Selection
    {
        get { return selection; }
        set { selection = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public int Goal
    {
        get { return goal; }
        set { goal = value; }
    }


}
