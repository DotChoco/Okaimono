using DCTI.Components;
using DCTI.Models;
using DCTI.Structs;
using DCTI.Structs.Enums;

namespace Okaimono;

// Components:
//     Table -> checked
//     InputField -> checked
//     Text -> checked
//     CheckBox -> checked
//     Tree -> checked
//     Region -> checked

public sealed class Render {
    public Render()
    {
        Console.Clear();
        Console.WriteLine("DCTI Render");

        
        // var data = new InputFieldData()
        //     Placeholder = new()
        //     {
        //         color = "879EA6",
        //         value = "Enter Text",
        //     },
        //     ContentColor = Color.DEFAULT_COLOR,
        //     ComponentColor = "FE94FF",
        //     Style = FieldStyles.Rounded,
        // };
        //
        // InputField inputField = new(data);
        
        // inputField.SetSize(25,1)
        //     .SetPosition(new(10,5))
        //     .Render();
        //
        // inputField.ReadInput();

        MText [,] content = {
            {new(){value = "hola"}, new(){value = "como"}, new(){value = "estas"}},
            {new(){value = "yo"}, new(){value = "cansaeroddda"}, new()},
            {new(), new(){value = "por"}, new(){value = "preguntar"}}
        };
        
        TbContent ad = new(){
            Content = content,
            SeparetedRows = true,
            Style = FieldStyles.Rounded,
        };
        Table tabless = new(ad);
        tabless.SetPosition(new(5,4)).Render();

        
        // TbContent ads = new(){
        //     Content = content,
        //     SeparetedRows = true,
        //     Style = FieldStyles.Rounded
        // };
        //
        // Table table = new(ads);
        // table.SetPosition(new(5,14)).Render();
        //
        // TbContent aa = new(){
        //     Content = content,
        //     SeparetedRows = true,
        //     Style = FieldStyles.Box
        // };
        //
        // Table tables = new(aa);
        // tables.SetPosition(new(5,24)).Render();
        
        // CheckBox checkBox = new(new(){value = "IsLive", color = "6F8833"});
        // checkBox.SetPosition(new(5,15)).Render();
        //
        // System.Threading.Thread.Sleep(2000);
        // checkBox.Toggle();
        // System.Threading.Thread.Sleep(2000);
        // checkBox.Toggle();
        
        
        // Region region = new();
        // region.SetSize(new())
        //     .SetPosition(new(5,15))
        //     .Render();
        
        
    }


}


