RULE AND RECOMMENDATION
-----------------------
## 1. Sheet name
- `%Constants` (end with "Constants") exports to a [.cs] file containing Constants
- `%IDs` (end with "IDs") exports to a [.cs] file containing Constants
- `Localization%` (start with "Localization") exports to a json Localization file
- In other cases, data in Json format will be exported to a [.txt] file.

## 2. Naming rules for constants in the IDs sheet
- Should not contain spaces or accents
- Preferably write all letters in uppercase (not mandatory)
- For enum types, add the suffix `[enum]` to the name of the group (column)
- When adding the suffix `[enum]`, the IDs file will export both Constants and Enum. If you only want to export Enum and not Constants, select `Only Enum as IDs` in the Settings tab.

## 3. Naming rules for columns in the Table Data sheet
- Should not contain spaces or special characters, and should have unique names
- If the suffix `[]` is added, the value will be an array/list. Elements of the array/list are separated by `|`, `:`, or a newline.
- If the suffix `{}` is added, the value will be a string that can be converted to Json Data.
- If the suffix `[x]` is added, the entire data of that column will be ignored and not exported.

## Tips
- Separating Constants, IDs, and Localizations files: By default, the Constants, IDs, and Localizations sheets will be exported and merged into a single file based on their types. To export them separately, check the corresponding boxes for Separate IDs, Separate Constants, or Separate Localizations in the Settings tab.
- Exporting to separate Excel files: To export to single Excel file or export a specific sections of an Excel file, use the `Convert Single Excel` tab.
- When exporting Json Data, columns (fields) with empty or zero values will be excluded. If you want to keep those empty columns, add their names to the `Unminimized` Fields box in the Settings tab.
- When you want to skip exporting a specific sheet in the Excel file, simply add a blank row at the top or add the name of that sheet to the Excluded Sheets box in the Settings tab.
- Check the `One Json - One Excel` checkbox in the Settings tab to merge all data sheets in an Excel file into a single Json file. The resulting file will be named by the Excel file name.
- Data encryption:  Currently, the tool only supports encrypting Json Data. To encrypt the data, simply check the `Encrypt Json Data` box in the Settings tab.

## Attribute system
The attribute system allows for unlimited data expansion and easy management. Suitable for games with complex stat systems or diverse item systems.
### Attribute naming rules:
Attribute data columns must be located at the end of the data table, and the data for an attribute starts from the column with the name containing `attribute`. The relative order is as follows:
1. `attribute%`: Attribute ID, these IDs must be set up in the IDs sheet beforehand. It must contain the string "attribute" followed by any suffix, but it is recommended to use sequential numbers starting from 0.
2. `value%` / `value%[]`: Attribute value, must be of type Number. It can be an array, in that case it should have `[]`.
3. `increase%` / `increase%[]`: Additional value for the attribute, used when the attribute can level up and increase stats. It can be an array, in that case it should have `[]`.
4. `unlock%` / `unlock%[]`: Additional value for the attribute, used when the attribute requires conditions to unlock. It can be an array, in that case it should have `[]`.
5. `max%` / `max%[]`: Additional value for the attribute, used when the attribute has a maximum value. It can be an array, in that case it should have `[]`.
```
- Example 1: attribute0, value0, increase0, value0, max0.
- Example 2: attribute1, value1[], increase1[], value1[], max1[].
```