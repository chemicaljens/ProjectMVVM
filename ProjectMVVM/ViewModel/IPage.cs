using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVVM.ViewModel
{

    //elke view model klasse zal deze interface moeten inplementeren
    //zo kan ik later een lijst van objecten van klassen gaan bij houden
    //waarvan de klasse deze interface inplementeert. 
    //Deze lijst it in de klass Application VM
    interface IPage
    {
        //één property in steken elke klasse moet deze property gaan uitwerken.
        String Name { get; }
    }
}
