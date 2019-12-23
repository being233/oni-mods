using Harmony;
using UnityEngine;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateElementEntries))]
    public static class GenerateElementEntries
    {
        public static string GetStateString(Element.State state)
        {
            switch (state & Element.State.Solid)
            {
                case Element.State.Solid:
                    return Element.State.Solid.ToString();
                case Element.State.Liquid:
                    return Element.State.Liquid.ToString();
                case Element.State.Gas:
                    return Element.State.Gas.ToString();
                default:
                    return "OTHER";
            }
        }

        public static void Postfix()
        {
            Debug.Log("GenerateElementEntries");
            foreach (var element in ElementLoader.elements)
            {
                if (element.disabled) continue;

                var name = element.id.ToString();
                var state = GetStateString(element.state);

                Tuple<Sprite, Color> tuple;

                switch (element.id)
                {
                    case SimHashes.Void:
                        tuple = new Tuple<Sprite, Color>(Assets.GetSprite("ui_elements-void"), Color.white);
                        break;
                    case SimHashes.Vacuum:
                        tuple = new Tuple<Sprite, Color>(Assets.GetSprite("ui_elements-vacuum"), Color.white);
                        break;
                    default:
                        tuple = Def.GetUISprite(element);
                        break;
                }

                var sprite = tuple.first;
                var color = tuple.second;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/ELEMENTS", state.ToUpper(), name.ToUpper()},
                    sprite = sprite,
                    color = color,
                });
            }
        }
    }
}