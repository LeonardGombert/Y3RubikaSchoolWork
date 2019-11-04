using System;


public class PropertyReader
{
    //simple struct to store the type and name of variables
    public struct Variable
    {
        public string name;
        public Type type;
    }

    //for instances of classes that inherit PropertyReader
    private Variable[] _fields_cache;
    private Variable[] _props_cache;

    public Variable[] getFields()
    {
        if (_fields_cache == null)
            _fields_cache = getFields(this.GetType());

        return _fields_cache;
    }

    public Variable[] getProperties()
    {
        if (_props_cache == null)
            _props_cache = getProperties(this.GetType());

        return _props_cache;
    }

    //getters and setters for instance values that inherit PropertyReader
    public object getValue(string name)
    {
        return this.GetType().GetProperty(name).GetValue(this, null);
    }

    public void setValue(string name, object value)
    {
        this.GetType().GetProperty(name).SetValue(this, value, null);
    }

    //static functions that return all values of a given type
    public static Variable[] getFields(Type type)
    {
        var fieldValues = type.GetFields();
        var result = new Variable[fieldValues.Length];
        for (int i = 0; i < fieldValues.Length; i++)
        {
            result[i].name = fieldValues[i].Name;
            result[i].type = fieldValues[i].GetType();
        }

        return result;
    }

    public static Variable[] getProperties(Type type)
    {
        var propertyValues = type.GetProperties();
        var result = new Variable[propertyValues.Length];
        for (int i = 0; i < propertyValues.Length; i++)
        {
            result[i].name = propertyValues[i].Name;
            result[i].type = propertyValues[i].GetType();
        }

        return result;
    }
}