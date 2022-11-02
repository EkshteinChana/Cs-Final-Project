// See https://aka.ms/new-console-template for more information
char choice;


void menue(string type)
{
    string specialOptions = ".";
    if(type == "order item") { specialOptions = $@"f for display all the items of existing orders "; };
    if (type == "order") { specialOptions = $@"f for display all the items in the order
                                               g for display a specific item of the order "; }
    Console.WriteLine($@"
    Choose the following action:
    a for adding an {type},
    b for display an {type},
    c for display the {type}s list,
    d for updating an {type},
    e for deleting an {type} from the {type}s list
    {specialOptions}
    ");
    
    choice = Console.ReadKey().KeyChar;
    switch( choice)
    {
        case 'a': //add

            break;
    }
}

void main(){
    bool toContinue = true;
    while (toContinue)
    {
        Console.WriteLine($@"
            Press 0 to exit,
            1 to check the orders list,
            2 to check the orders list,
            3 to check the orders items list.
        ");
        choice = Console.ReadKey().KeyChar;
        switch (choice)
        {
            case '0':
                toContinue = false;
                break;
            case '1':    
                menue("order");
                break;

        }
    }
}
enum options { add = 'a' };