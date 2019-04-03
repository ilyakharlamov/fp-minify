let rec minify lines = 
    match Seq.length lines with
    | 0 -> ""
    | _ ->
        let currentline = Seq.head lines
        currentline + (Seq.tail lines |> minify)

[<EntryPoint>]
let main argv = 
    match argv.Length with
    | 1 -> 
        seq { yield! System.IO.File.ReadAllLines argv.[0]}
            |> minify
            |> printfn "%s"
        0
    | _ -> 
        printfn "Expecting only one argument"
        1