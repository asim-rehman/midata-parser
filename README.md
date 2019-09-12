# MiData Parser

https://www.gov.uk/government/news/the-midata-vision-of-consumer-empowerment

MiData is a simple console based application which will parse MiData current account files supported by major banking providers in UK. The application will organise a MiData file by Month-Year and categories expense/income by categories. You can export in either excel (recommended) or CSV.

It requires two core functionalites to run, 1 is the Settings.JSON file which specifies certain settings, pretty self explantory.
## Settings.JSON
      {

        "OutputPath": "S:\\Users\\8501\\",
        "InputPath": "SampleData.csv",
        "Format": "excel",
        "Log": false,
        "LogDirectory": "C:\\MiData.txt",
        "Display": true,
        "ModelFile": "Model.json",
        "Delimiter": ";"
      }
      
The delimiter is how the input file is being split, some banks use a comma while others use semi-colon. 

The next file is Model.JSON (for output of excel only), this allows you to categories expenses, when the excel is generated it will contain several sheets corresponding the the Model data. The **type** is either *Expense* or *Income* however, at the moment it serves no purpose. The content in Items in searched in both Description columns and payment type (or Merchant)
## Model.JSON

    {
        "Sheets":[{
            "Name":"Supermarkets",
            "Items": [ "Walmart", "Iceland" ],
            "Type":"Expense"
        },
        {
            "Name":"e-commerce",
            "Items":["Amazon", "eBay"],
            "Type":"Expense"
        }]
    }
