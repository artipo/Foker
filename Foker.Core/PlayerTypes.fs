namespace Foker.Core

open Foker.Core.CardTypes
open Foker.Core.Utils

module PlayerTypes =
    type Hand = Card list
    
    [<StructuredFormatDisplay("{StringDisplay}")>]
    type Player =
        { id : int; hand : Hand}
        
        member this.StringDisplay =
            $"p_{this.id} with {formatList (fun c -> c.ToString()) this.hand}"
            
        override this.ToString () =
            this.StringDisplay

    type Table = Card list
    
    let initDrawForTable (deck : Deck) : Table * Deck =
        let rec loop acc deck =
            function
            | 0 ->
                acc, deck
            | i ->
                let card, remainingDeck = draw deck
                match card with
                | Some c -> 
                    loop (c::acc) remainingDeck (i - 1)
                | None ->
                    acc, deck
        
        loop [] deck 3
        
    let drawForTable (table : Table) (deck : Deck) : Table * Deck =
        match deck with
        | [] ->
            table, deck
        | _ ->
            let card, remainingDeck = draw deck
            match card with
            | Some c -> 
                (table @ [ c ]), remainingDeck
            | None ->
                table, deck
    
    let initDrawForPlayer (deck : Deck) (playerIndex : int) : Player * Deck =
        let rec loop acc deck =
            function
            | 0 ->
                acc, deck
            | i ->
                let card, remainingDeck = draw deck
                match card with
                | Some c -> 
                    loop { acc with hand = (c::acc.hand) } remainingDeck (i - 1)
                | None ->
                    acc, deck
        loop { id = playerIndex ; hand = [] } deck 2
    
    let initDrawForPlayers (deck : Deck) (nPlayers : int) : Player list * Deck =
        let rec loop acc deck =
            function
            | 0 ->
                acc, deck
            | i ->
                let p, remainingDeck = initDrawForPlayer deck i
                loop (p::acc) remainingDeck (i - 1)
                
        loop [] deck nPlayers
    
    let drawForPlayer (player : Player) (deck : Deck) : Player * Deck =
        match deck with
        | [] ->
            player, deck
        | _ ->
            let card, remainingDeck = draw deck
            match card with
            | Some c -> 
                { player with hand = player.hand @ [ c ] }, remainingDeck
            | None ->
                player, deck
    
    let drawForPlayers (players : Player list) (deck : Deck) : Player list * Deck =
        let rec loop acc deck =
            function
            | [] ->
                acc, deck
            | player :: xs ->
                let p, remainingDeck = drawForPlayer player deck 
                loop (acc @ [ p ]) remainingDeck xs
                
        loop [] deck players