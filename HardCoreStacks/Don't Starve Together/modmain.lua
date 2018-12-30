--Function that sets the max stackable size of the item to the size set in the configuiration
function SetSize(inst)
  inst.components.stackable.maxsize = GetModConfigData("size")
end

--Sets indivual items to have the component "stackable" as well as setting the size of the stack
function SetSizePost(inst)
  inst:AddComponent("stackable")
  inst.components.stackable.maxsize = GetModConfigData("size")
end

--Tunes the max stack size to the mod configuration data to the size set
TUNING.STACK_SIZE_LARGEITEM = GetModConfigData("size")
TUNING.STACK_SIZE_MEDITEM = GetModConfigData("size")
TUNING.STACK_SIZE_SMALLITEM = GetModConfigData("size")

--Setting the indidual items to be able to stack if the mod configuration is set to yes
if GetModConfigData("StackIndividual")=="Yes" then
  AddPrefabPostInit("rabbit",SetSizePost)
  AddPrefabPostInit("robin", SetSizePost)
  AddPrefabPostInit("robin_winter", SetSizePost)
  AddPrefabPostInit("crow", SetSizePost)
  AddPrefabPostInit("parrot", SetSizePost)
  AddPrefabPostInit("seagull", SetSizePost)
  AddPrefabPostInit("toucan", SetSizePost)
  AddPrefabPostInit("crab", SetSizePost)
  AddPrefabPostInit("jellyfish", SetSizePost)
  AddPrefabPostInit("snakeoil", SetSizePost)
  AddPrefabPostInit("coral_brain", SetSizePost)
  AddPrefabPostInit("lobster", SetSizePost)
  AddPrefabPostInit("heatrock", SetSizePost)
end
