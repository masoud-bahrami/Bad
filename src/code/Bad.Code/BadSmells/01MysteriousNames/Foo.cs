
// Meaningless names

using System;

namespace Bad.Code.BadSmells._01MysteriousNames;

public class Foo
{
    // Meaningless names
    public void Bar()
    {

    }

    // lack of precision
    private object data { get; set; }
    // too abstract
    private object data1 { get; set; }
    // toooo abstract
    private object data2 { get; set; }

    // what does it mean
    // Does it mean "point of sale"?
    public object pos { get; set; }
    // what does it mean
    // Does it mean "accumulator"?
    public object acc { get; set; }

    // it depends on the context.
    // in an supply chain context order is not a right name, it might be related
    // to a shipment!
    public object order { get; set; }

    // It isn't the same as Broker  
    public object Carried { get; set; }

    // replace with more specific names, such as: Employee, Person, shareholder
    public object CompanyPerson { get; set; }

    // Vestigial Hungarian notation. Perhaps 'created' is just enough
    public DateTime DateCreated { get; set; }

    // variable is a long integer ("l");
    public object lAccountNum { get; set; }
    // variable is an array of unsigned 8-bit integers ("arru8");
    public object arru8NumberList { get; set; }
    // variable represents a row ("rw");
    public object rwPosition { get; set; }
    // variable represents an unsafe string ("us"), which needs to be "sanitized" before it is used (e.g. see code injection and cross-site scripting for examples of attacks that can be caused by using raw user input)
    public object usName { get; set; }
    // variable is a zero-terminated string ("sz"); this was one of Simonyi's original suggested prefixes.
    public object szName { get; set; }
}