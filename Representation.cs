using System;

public class Representation
{
    private string country;
    private string continent;
    private string ihfRang;
    private int groupPosition;
    private string group;
    public Representation(string dataCountry, string dataContinent, string dataIhfRang, int datagroupPosition = 0, string dataGroup = "null")
	{
		country = dataCountry;
		continent = dataContinent;
		ihfRang = dataIhfRang;
        groupPosition = datagroupPosition;
        group = dataGroup;

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


}
