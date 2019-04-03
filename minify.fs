let alphanumerics = System.Text.RegularExpressions.Regex("[a-zA-Z][\\w\\d]*")

let keywords = set ["var";  "function"; "return"; "if"; "else"]

let rec minify varreplacements lines = 
    match Seq.length lines with
    | 0 -> ""
    | _ ->
        let (currentline:string) = Seq.head lines
        let varreplacements' = seq { for x in alphanumerics.Matches currentline -> x.Value }
                                |> Seq.filter (fun oldname -> not(Map.containsKey oldname varreplacements || Seq.contains oldname keywords))
                                |> Seq.fold (fun sofar oldname -> Map.add oldname (Map.count sofar + 97 |> char |> string) sofar) varreplacements
        let newname oldname = 
            match Map.tryFind oldname varreplacements' with
            | Some x -> x
            | None -> oldname
        alphanumerics.Replace(currentline, fun mtch -> newname mtch.Value) + (Seq.tail lines |> minify varreplacements')

[<EntryPoint>]
let main argv = 
    match argv.Length with
    | 1 -> 
        seq { yield! System.IO.File.ReadAllLines argv.[0]}
            |> minify Map.empty
            |> printfn "%s"
        0
    | _ -> 
        printfn "Expecting only one argument"
        1