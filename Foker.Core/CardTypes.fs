namespace Foker.Core

open System
open System.Runtime.CompilerServices

module CardTypes =

    type Suit =
        | Hearts
        | Diamonds
        | Clubs
        | Spades

    type Rank =
        | Ace
        | King
        | Queen
        | Jack
        | Ten
        | Nine
        | Eight
        | Seven
        | Six
        | Five
        | Four
        | Three
        | Two

    [<StructuredFormatDisplay("{StringDisplay}")>]
    type Card =
        { suit: Suit; rank: Rank }
        
        member this.StringDisplay =
            $"{this.rank} of {this.suit}"
            
        override this.ToString () =
            this.StringDisplay

    /// Deck is an ordered list of cards.
    /// deck.[0] is the top card, the first that will be drawn.
    type Deck =
        Card list

    let suits = [ Hearts; Diamonds; Clubs; Spades ]
    
    let ranks =
        [ Ace
          King
          Queen
          Jack
          Ten
          Nine
          Eight
          Seven
          Six
          Five
          Four
          Three
          Two ]
    
    let fullDeck =
        [ for suit in suits do
              for rank in ranks do
                  yield { suit = suit; rank = rank } ]

    let draw (deck : Deck) : Card option * Deck =
        match deck with
        | [] ->
            (None, deck)
        | x :: xs ->
            (Some x, xs)

    let drawAtIndex (deck : Deck) (index : int) : Card option * Deck =
        match deck with
        | [] ->
            (None, deck)
        | _ ->
            if index < deck.Length then
                let c = deck.[index]
                let d = deck |> List.except [ c ]
                (Some c, d)
            else
                (None, deck)

    let drawRandom (deck : Deck) : Card option * Deck =
        match deck with
        | [] ->
            (None, deck)
        | _ ->
            let r = Random()
            let i = r.Next(deck.Length)
            drawAtIndex deck i

    let add (deck : Deck) (card: Card) : Deck =
        [ card ] @ deck 
    
    let shuffle (deck : Deck) : Deck =
        let rec loop newDeck =
            function
            | [] ->
                newDeck
            | deck ->
                let card, remainingDeck = drawRandom deck
                match card with
                | Some c -> 
                    loop (add newDeck c) remainingDeck
                | None ->
                    newDeck
        
        loop [] deck
    