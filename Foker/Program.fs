// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Foker.Core.CardTypes
open Foker.Core.PlayerTypes
open Foker.Core.Utils

let printList<'a> (list : 'a list ) =
    formatList (fun c -> c.ToString()) list
    |> Console.WriteLine

[<EntryPoint>]
let main argv =
    let deck = fullDeck |> shuffle
    
    Console.Write "Welcome to Foker, please enter the number of players: "
    let nPlayers = Console.ReadLine() |> int
    
    Console.WriteLine "PHASE 0"
    
    let table, deck = initDrawForTable deck
    let players, deck = initDrawForPlayers deck nPlayers
    
    Console.WriteLine "    Table:"
    printList table
    Console.WriteLine "    Players:"
    printList players

    Console.WriteLine ""
    Console.WriteLine "PHASE 1"
    
    let table, deck = drawForTable table deck
    
    Console.WriteLine "    Table:"
    printList table
    
    Console.WriteLine ""
    Console.WriteLine "PHASE 2"
    
    let table, deck = drawForTable table deck
    
    Console.WriteLine "    Table:"
    printList table
    
    0 // return an integer exit code