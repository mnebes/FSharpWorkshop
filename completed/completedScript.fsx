open System
open System.IO

let readLines = File.ReadAllLines "shoppingcartpositions.csv"

// 2. CLEANING UP HEADERS

let dropHeader (x:_[]) = x.[1..]
let linesWithoutHeader = dropHeader readLines

// 3. EXTRACTING COLUMNS
let splitLines = linesWithoutHeader |> Array.map (fun column -> column.Split(','))

 
// 4. MODELING WITH TYPES

type ShoppingCartItemData = { CustomerId: int; ItemId: int; ItemName: string; Count: int; UnitPrice: decimal }
let shoppingCartItems = 
    splitLines 
    |> Array.map (fun line -> 
    {
        CustomerId = (int)line.[0]
        ItemId = (int)line.[1]
        ItemName = line.[2]
        Count = (int)line.[3]
        UnitPrice = Convert.ToDecimal(line.[4])
    })

type ShoppingCartItem = { ItemId: int; ItemName: string; Count: int; UnitPrice: decimal }
type ShoppingCart = { CustomerId: int; ShoppingCartItems: ShoppingCartItem[] }

let shoppingCarts = 
    shoppingCartItems 
    |> Array.groupBy (fun itemData -> itemData.CustomerId)
    |> Array.map (fun (customerId, itemDatas) -> 
        {
            CustomerId = customerId; 
            ShoppingCartItems = 
                itemDatas |> Array.map (fun itemData -> 
                    {ItemId = itemData.ItemId; ItemName = itemData.ItemName; Count = itemData.Count; UnitPrice = itemData.UnitPrice})
        })

// 5. VALIDATION

let amountIsNonPositive shoppingCartItem =
    shoppingCartItem.Count <= 0

let priceIsNegative shoppingCartItem =
    shoppingCartItem.UnitPrice < 0.0m

let validShoppingCarts =
    shoppingCarts
    |> Array.filter (fun shoppingCart -> shoppingCart.ShoppingCartItems |> Array.exists amountIsNonPositive |> not)
    |> Array.filter (fun shoppingCart -> shoppingCart.ShoppingCartItems |> Array.exists priceIsNegative |> not)

// 6. ORDER CREATION

type SalesPosition = { Name:string; ItemCount:int; UnitPrice:decimal }
type DiscountPosition = { Name:string; Amount:decimal }
type OrderPosition =
    | SalesPosition of SalesPosition
    | DiscountPosition of DiscountPosition
type Order = 
    {
        CustomerId:int
        OrderDate:DateTime
        OrderPositions: OrderPosition[]
    }

let mapToOrder (shoppingCart: ShoppingCart) =
    {
        CustomerId = shoppingCart.CustomerId
        OrderDate = DateTime.Now
        OrderPositions =
            shoppingCart.ShoppingCartItems
            |> Array.map (fun item -> SalesPosition { Name=item.ItemName; ItemCount=item.Count; UnitPrice=item.UnitPrice } )
    }

let orders =
    validShoppingCarts
    |> Array.map mapToOrder

let calculateSum order =
    order.OrderPositions
    |> Array.fold (fun acc position -> 
        match (position) with
        | SalesPosition sp -> acc + (decimal)sp.ItemCount * sp.UnitPrice
        | DiscountPosition dp -> acc - dp.Amount
    ) 0.0m

let addDiscount order =
    let discount = DiscountPosition { Name = "super discount"; Amount = 20.0m }
    { order with OrderPositions = Array.append order.OrderPositions [| discount|] }

let addDiscountIfQualifies order =
    let sum = calculateSum order
    if (sum > 1000m) then
        addDiscount order
    else
        order    

orders
|> Array.map calculateSum

let ordersWithDiscounts =
    orders
    |> Array.map addDiscountIfQualifies
    |> Array.map calculateSum