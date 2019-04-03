[<EntryPoint>]
let main argv = 
    match argv.Length with
    | 1 -> 
        printfn "you asked to minify file %s" argv.[0]
        0
    | _ -> 
        printfn "Expecting only one argument"
        1