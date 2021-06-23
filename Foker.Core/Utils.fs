namespace Foker.Core

module Utils =
    
    type ItemFormatter<'a> =
        'a -> string
    
    let formatList<'a> (formatter : ItemFormatter<'a>) (items : 'a list) : string =
        items
        |> List.map formatter
        |> String.concat "; "
        |> sprintf "[ %s ]"