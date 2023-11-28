Download via [Releases page](https://github.com/nbhung100914/excel-to-unity/releases)

You can download the demo project [Here](https://github.com/nbhung100914/excel-to-unity-example)

# 1. Introduction

The core idea behind this tool is to create a tool that serves Indie Game Developers. This is a tool that helps design and manage databases that both Dev and Designer can use. Designers can easily check the stats in the game without the support of Dev.

Game projects, regardless of large or small scale, all need a Static Database. Depending on the scale of the project, the number of Data Tables increases, Constants and Ids will also increase accordingly. The problem will become more complicated if there is no effective management solution. Therefore, the initial goal of this tool is to manage IDs, Constants, and find a way to easily search, change IDs and Constants in a single place where the Data Table is also updated accordingly.

After a long expansion process, the tool has added many new data types, taking advantage of the power of Excel for design and management. Although this tool is mainly developed for RPG games - a genre of games that usually have a large Static Database, it can be completely applied to other game genres that need a large Static Database.

# 2. Main functions

- Support for managing the Database entirely with Excel.
- Efficiently manage IDs and Constants, allowing for batch adjustments without affecting the Database.
- Easily manage multiple languages through the Localization system, which can be powerfully integrated with Unity.
- Export Data Table to JSON Data files for easy integration into Unity.
- Provide many data formats, which can be flexibly changed according to Data design requirements.

# 3. Introduce features and installation instructions

## 3.1. Export single excel

![excel-2-unity-tab-1](https://github.com/nbhung100914/excel-to-unity/assets/9100041/e1a9f646-fe47-480b-a71b-4a0e79f627ca)

This is a basic but very important function, helping you get acquainted with the tools. If your Static Database is not too large or complex, and only needs an excel file to contain all data, then this function is enough for your needs. However, if your Static Database is complex and needs to be stored in multiple Excel files, you will need to use the **Export Multi Excels** function. This function will be introduced in the next section.

The important functions are in the buttons on the right:

- **Export IDs:** export IDs sheets to C# files.
- **Export Constants:** export Constants sheets to C# files.
- **Export Localization:** export Localization Data and corresponding Localization Component, Localization API.
- **Export Json:** export Data Table sheets to JSON Data.

Settings:

- **Data File:** enter the address of the excel file containing the Static Database.
- **Separate Constants:** select this option if you want to export _[%IDs]_ and _[%Constants]_ sheets to separate C# files, with file names structured as _[SheetName] + IDs.cs_ and _[SheetName] + Constants.cs_.
- **Encrypt Data:** select this option if you want to encrypt JSON Data exported from Data Table.
- **Merge Jsons:** select this option if you want to export all Data Tables to a single json file. The json file name will be the excel file name or a custom name in the adjacent TextBox.

Display information:

- The table on the left displays a list of sheets in the excel file.
- The table in the middle displays a list of ids compiled from sheets named _[%IDs].cs_.
- The scroll box below will display the log.

## 3.2. Export multi excels (All in one)

![excel-2-unity-tab-2](https://github.com/nbhung100914/excel-to-unity/assets/9100041/82288317-29d9-4a1a-aadd-6bf730fa2e82)

This is a comprehensive function, everything will be processed with just one button press:

1. Select all the excel files you want to process.
2. You can decide whether to export IDs, Constants or not by checking the corresponding CheckBox for each excel file.
3. Finally, press the Export All button.

## 3.3. Settings

![excel-2-unity-tab-3](https://github.com/nbhung100914/excel-to-unity/assets/9100041/a0268340-6198-4e38-bc97-c04f900ef2eb)

- **Json Data Output:** The data table will be converted to JSON Data and saved at this address.
- **Constants Output:** IDs, Constants, Localization Component, and Localization API will be saved at this address.
- **Localization Output:** Localization Data will be saved at this address. This address should be in the _Resources_ folder.
- **Namespace:** Declare the namespace for the C# files that the tool exports.
- **Separate IDs:**
  - TRUE: Export _[%IDs]_ sheets to independent C# files with file names structured as _[SheetName] + IDs.cs_
  - FALSE: Consolidate all _[%IDs]_ sheets from all excel files into one and then export to a single C# file named _IDs.cs_
- **Separate Constants:**
  - TRUE: Export _[%Constants]_ sheets to independent C# files with file names structured as _[SheetName] + %Constants.cs_
  - FALSE: Consolidate all _[%Constants]_ sheets from all excel files into one and then export to a single C# file named _Constants.cs_
- **Separate Localization:**
  - TRUE: Export _[%Localization%]_ sheets to independent groups, each group includes Localization Data, Component, and API, the file names will have the following structure:
    - Localization Data: _[SheetName]\_[language].txt_
    - Component: _[SheetName] + Text.cs_
    - API: _[SheetName].cs_
  - FALSE: Consolidate all _[%Localization%]_ sheets from all excel files into one and then export to a single group, the file names will have the following structure:
    - Localization Data: _Localization\_ + [language].txt_
    - Component: _LocalizationText.cs_
    - API: _Localization.cs_
- **Encrypt Json:** Encrypt JSON Data before exporting to a text file
- **Only enum as ID:** Applies to _[%IDs]_ sheets, columns with the extension _[enum]_. If selected, that IDs column will be exported as enum and ignore the Integer Constant form.
- **One Json - One Excel:** Consolidate Data Table in one excel file into a single json file, the file name is structured as _[ExcelName].txt_
- **Encryption Key:** Key to encrypt JSON Data
- **Language maps:** Applies to Localization combined with TextMeshPro, used to compile the character table of a language. Mainly applied for Korean, Japanese, and Chinese, these are languages with an extremely large character system.
- **Excluded Sheets:** Enter the names of Sheets to be excluded when processing Data Table.
- **Excluded Sheets:** Enter the names of fields to be excluded when processing Data Table.

## 3.4. Encrypt & Decrypt Text

![excel-2-unity-tab-4](https://github.com/nbhung100914/excel-to-unity/assets/9100041/58034c2f-97e3-44e5-907d-559294960358)

This function allows you to encrypt or decrypt a string of characters based on the Key provided in the Settings Tab. You can use this function to secure the content of a text, or to open and read the encrypted JSON Data files after they have been exported.

# 4. Data Design Rules in Excel

## 4.1. IDs

[Download detail example from GitHub](https://github.com/nbhung100914/excel-to-unity/blob/main/Example.xlsx)

| Hero   |     |         | Building      |     |         | Pet      |     |         | Gender[enum]      |     |
| ------ | --- | ------- | ------------- | --- | ------- | -------- | --- | ------- | ----------------- | --- |
| HERO_1 | 1   | comment | BUILDING_NULL | 0   | comment | PET_NULL | 0   | comment | GENDER_NONE       | 0   |
| HERO_2 | 2   | comment | BUILDING_1    | 1   |         | PET_1    | 1   |         | GENDER_MALE       | 1   |
| HERO_3 | 3   | comment | BUILDING_2    | 2   |         | PET_2    | 2   |         | GENDER_FEMALE     | 2   |
|        |     |         | BUILDING_3    | 3   |         | PET_3    | 3   |         | GENDER_HELICOPTER | 3   |
|        |     |         | BUILDING_4    | 4   |         | PET_4    | 4   |         |                   |     |
|        |     |         | BUILDING_5    | 5   |         | PET_5    | 5   |         |                   |     |
|        |     |         | BUILDING_6    | 6   |         | PET_6    | 6   |         |                   |     |
|        |     |         | BUILDING_7    | 7   |         | PET_7    | 7   |         |                   |     |
|        |     |         | BUILDING_8    | 8   |         |          |     |         |                   |     |

Sheets named according to the syntax _[%IDs]_ are called IDs sheets. They are used to compile all ids into Integer Constants. The design rules are as follows:

- The sheet name needs to have `IDs` as a prefix or suffix.
- In this Sheet, only use the Integer data type.
- Each group is arranged in 3 consecutive columns.
- The first row contains the group name for easy lookup.
- The first column contains the Key Name, and the next column contains the Key Value.
- Key Value must be an integer.
- By default, all ids of a column will be exported as Integer Constants, but you can also export them as enum by adding the suffix [enum] to the group name.
- You can choose to only export enum and ignore Integer Constant by selecting `Only enum as IDs` in the Settings section.

```
| Group | Key | Comment |
| ----- | --- | ------- |
```

## 4.2. Constants

| Name                  | Type        | Value              | Comment               |
| --------------------- | ----------- | ------------------ | --------------------- |
| EXAMPLE_INT           | int         | 83                 | Integer Example       |
| EXAMPLE_FLOAT         | float       | 1.021              | Float example         |
| EXAMPLE_STRING        | string      | 321fda             | String example        |
| EXAMPLE_INTARRAY_1    | int-array   | 4                  | Integer array example |
| EXAMPLE_INT_ARRAY_2   | int-array   | 0:3:4:5            | Integer array example |
| EXAMPLE_FLOAT_ARRAY_1 | float-array | 5                  | FLoat array example   |
| EXAMPLE_FLOAT_ARRAY_2 | float-array | 5:1:1:3            | FLoat array example   |
| EXAMPLE_VECTOR2_1     | vector2     | 1:2                | Vector2 example       |
| EXAMPLE_VECTOR2_2     | vector2     | 1:2:3              | Vector2 example       |
| EXAMPLE_VECTOR3       | vector3     | 3:3:4              | Vector3 example       |
| EXAMPLE_REFERENCE_1   | int         | HERO_1             | Integer example       |
| EXAMPLE_REFERENCE_2   | int-array   | HERO_1 : HERO_2    | Integer array example |
| EXAMPLE_REFERENCE_3   | int-array   | HERO_1 \| HERO_3   | Integer array example |
| EXAMPLE_REFERENCE_4   | int-array   | HERO_1 HERO_4      | Integer array example |
| EXAMPLE_FORMULA_1     | int         | =1\*10\*36         | Excel formula example |
| EXAMPLE_FORMULA_2     | float       | =1+2+3+4+5+6+7+8+9 | Excel formula example |

Sheets named according to the syntax _[%Constants]_ are called Constants Sheets. They are used to compile the Constants in the project. The table below will help you refer to all the data types that can be used in this sheet. The design rules are as follows:

- The sheet name needs to have `Constants` as a prefix or suffix.
- There are four columns: Name, Type, Value, and Comment.
- Name: This is the name of the constant, it must be written continuously, does not contain special characters, and should be capitalized.
- Type: This is the data type of the constant. You can use the following data types: `int`, `float`, `bool`, `string`, `int-array`, `float-array`, `vector2`, and `vector3`.
- Value: The value corresponding to the data type. For array data types, elements must be separated by `:` or `|` or `newline`.

```
| Name | Type | Value | Comment |
| ---- | ---- | ----- | ------- |
```

## 4.3. Localization

| idstring     | relativeId | english                   | spanish                        |
| ------------ | ---------- | ------------------------- | ------------------------------ |
| message_1    |            | this is english message 1 | este es el mensaje en ingles 1 |
| message_2    |            | this is english message 2 | este es el mensaje en ingles 2 |
| message_3    |            | this is english message 3 | este es el mensaje en ingles 3 |
|              |            |                           |                                |
| content      | 1          | this is english message 1 | este es el mensaje en ingles 1 |
| content      | 2          | this is english message 2 | este es el mensaje en ingles 2 |
| content      | 3          | this is english message 3 | este es el mensaje en ingles 3 |
|              |            |                           |                                |
| title_1      |            | this is english title 1   | este es el titulo 1 en ingles  |
| title_2      |            | this is english title 2   | este es el titulo 2 en ingles  |
| title_3      |            | this is english title 3   | este es el titulo 3 en ingles  |
|              |            |                           |                                |
| whatever_msg |            | this is a sample message  | este es un mensaje de muestra  |
|              |            |                           |                                |
| hero_name    | HERO_1     | hero name 1               | nombre del héroe 1             |
| hero_name    | HERO_2     | hero name 2               | nombre del héroe 2             |
| hero_name    | HERO_3     | hero name 3               | nombre del héroe 3             |

Sheets named according to the syntax _[%Localization%]_ are called Localization Sheets. The design rules are as follows:

- The sheet name needs to have `Localization` as a prefix or suffix.
- This sheet has a structure of 2 key columns, one is the main key `idString` and one is the additional key `relativeId`.
- The next columns will contain localized content.
- The key of a row is the combination of `idString` and `relativeId`.

```
| idString | relativeId | english | spanish | japan | .... |
| -------- | ---------- | ------- | ------- | ----- | ---- |
```

# 4.4 Data table - JSON Data

### Basic data type: Boolean, Number, String

| numberExample1 | numberExample2 | numberExample3 | boolExample | stringExample |
| -------------- | -------------- | -------------- | ----------- | ------------- |
| 1              | 10             | 1.2            | TRUE        | text          |
| 2              | 20             | 3.1            | TRUE        | text          |
| 3              | BUILDING_8     | 5              | FALSE       | text          |
| 6              | HERO_3         | 10.7           | FALSE       | text          |
| 9              | PET_2          | 16.4           | FALSE       | text          |

### Extended data type: Array, JSON object

| array1[]                | array2[]    | array3[]                       | array4[]              | array5[]   | array6[]    | JSON\{}                                                                   |
| ----------------------- | ----------- | ------------------------------ | --------------------- | ---------- | ----------- | ------------------------------------------------------------------------- |
| text1                   | 1           | 1                              | TRUE                  | 123<br/>66 | aaa<br/>ccc | \{}                                                                       |
| text2                   | 2 \| 2 \| 3 | 1 \| 2 \| 3                    | TRUE \| FALSE \| TRUE | 123<br/>71 | aaa<br/>ccc | \{"id":1, "name":"John Doe 1"}                                            |
| text1 \| text2          | 1 \| 2      | 1 \| BUILDING_2                | TRUE \| FALSE         | 123<br/>67 | aaa<br/>ccc | \{"id":2, "name":"John Doe 2"}                                            |
| text1 \| text2 \| text3 | 1 \| 2 \| 3 | BUILDING_1 \| HERO_2           | TRUE \| FALSE \| TRUE | 123<br/>68 | aaa<br/>ccc | \{"id":HERO_2, "name":"JohnDoe 2"}                                        |
| text3                   | 4 \| 2      | BUILDING_3 \| HERO_1 \| HERO_2 | TRUE \| FALSE         | 123<br/>76 | aaa<br/>ccc | [\{"id":HERO_1, "name":"John Doe 1"},\{"id":HERO_2, "name":"Mary Sue 2"}] |
| text1 \| text2 \| text7 | 5           | 1 \| 2 \| 4 \| PET_5           | TRUE                  | 123<br/>78 | aaa<br/>ccc | [\{"id":HERO_1, "name":"John Doe 1"},\{"id":HERO_2, "name":"Mary Sue 2"}] |

- For array type, the column name must have a suffix [].
- For JSON object type, the column name must have a suffix \{}.

### Special data type: Attributes list

| attribute0 | value0 | unlock0 | increase0 | max0 | attribute1 | value1[] | unlock1[] | increase1[] | max1[]   | ... | attributeN |
| ---------- | ------ | ------- | --------- | ---- | ---------- | -------- | --------- | ----------- | -------- | --- | ---------- |
| ATT_HP     | 30     | 2       | 1.2       | 8    |            |          |           |             |          | ... |            |
| ATT_AGI    | 25     | 3       | 1.5       | 8    |            |          |           |             |          | ... |            |
| ATT_INT    | 30     | 2       | 1         | 5    | ATT_CRIT   | 3 \| 2   | 0 \| 11   | 0.5 \| 1    | 10 \| 20 | ... |            |
| ATT_ATK    | 30     | 2       | 1         | 8    | ATT_CRIT   | 10 \| 1  | 1 \| 12   | 1.5 \| 1    | 10 \| 20 | ... |            |
|            |        |         |           |      | ATT_CRIT   | 10 \| 1  | 1 \| 12   | 1.5 \| 1    | 10 \| 20 | ... |            |

Attribute is a specific data type, specially created for RPG genre games - where characters and equipment can possess various different and non-fixed attributes and stats. This data type makes character and equipment customization more flexible, without restrictions.

![attribute example](https://github.com/nbhung100914/excel-to-unity/assets/9100041/2d619d56-5fa9-4371-b212-3e857bcbbead)

To define an attribute object type, the following rules should be followed:

- The attribute column should be placed at the end of the data table.
- Attribute id is a constant integer, so it should be defined in the IDs sheet.
- An attribute has the following structure:

  1. **`attribute`**: The column name follows the pattern _`attribute + (index)`_, where index can be any number, but should start from 0 and increase. The value of this column is the id of the attribute, which is an Integer type, this value should be set in the IDs sheet.
  2. **`value`**: The column name follows the pattern _`value + (index)`_ or _`value + (index) + []`_, the value of the column can be a number or a number array.
  3. **`increase`**: The column name follows the pattern _`increase + (index)`_ or _`increase + (index) + []`_. This is an additional value, which can be present or not, usually used for level-up situations, specifying the additional increase when a character or item levels up.
  4. **`unlock`**: The column name follows the pattern _`unlock + (index)`_ or _`unlock + (index) + []`_. This is an additional value, which can be present or not, usually used for situations where the attribute needs conditions to be unlocked, such as minimum level or minimum rank.
  5. **`max`**: The column name follows the pattern _`max + (index)`_ or _`max + (index) + []`_. This is an additional value, which can be present or not, usually used for situations where the attribute has a maximum value.

    ```
    Example 1: attribute0, value0, increase0, value0, max0.
    Example 2: attribute1, value1[], increase1[], value1[], max1[].
    ```
