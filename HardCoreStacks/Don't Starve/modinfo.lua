--Intial description of the mod
name ="Hardcore Stack Size"

description = "Sets the max stack size of 1-10 depending on configuration. Can also state if you want items like rabbits to be stackable."

author = "ImTom"

version ="0.1"
-- Don't want to sign up to the KEI forumthread. This currently routes to the homepage
forumthread = ""

--Setting the compatiabilities to all don't starve games
dont_starve_compatible = true
reign_of_giants_compatible = true
shipwrecked_compatible=true

--Read that it was supposed to be 4, but this works.
api_version = 6

--referencing the icons
icon_atlas = "modicon.xml"
icon = "hcstacks.tex"

--Setting the configuration options that are availabe and will be referenced later in the mod
configuration_options = {
  {
    --set the max stack size to any one of these options, the label is the in game labe, while name is the
    -- reference in the mod. It defaults to 1
    name ="size",
    label="Max Stack Size",
    options = {
      {description = "1",data =1},
      {description = "2",data =2},
      {description = "3",data =3},
      {description = "4",data =4},
      {description = "5",data =5},
      {description = "6",data =6},
      {description = "7",data =7},
      {description = "8",data =8},
      {description = "9",data =9},
      {description = "10",data =10},
    },
    default =1
  },
  {
    --See's if the user wants to stack individual items
    --defaults to no
    name = "StackIndividual",
    label="Stack Individual Items",
    options = {
        {description = "No",data ="No"},
        {description = "Yes",data ="Yes"},
      },
    default = "No"
  },
}
