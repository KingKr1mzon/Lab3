(*
Все исходные данные вводятся с клавиатуры, организовать проверку вводимых значений

Вариант 8
Вывести количество файлов, имя которых начинается с заданного
символа, в указанном каталоге (без подкаталогов).
*)

open System
open System.IO

/// Считывает с клавиатуры и проверяет существование пути к каталогу.
let rec readDirectoryPath () =
    printf "Введите путь к каталогу: "
    let path = Console.ReadLine()
    
    if String.IsNullOrWhiteSpace(path) then
        printfn "Ошибка: путь не может быть пустым. Попробуйте снова"
        readDirectoryPath ()
    elif not (Directory.Exists(path)) then
        printfn "Ошибка: указанный каталог не существует. Попробуйте снова"
        readDirectoryPath ()
    else
        path

/// Считывает с клавиатуры и проверяет целевой символ.
let rec readTargetCharacter () =
    printf "Введите один начальный символ для поиска: "
    let input = Console.ReadLine()
    
    if not (String.IsNullOrEmpty(input)) && input.Length = 1 then
        input.[0]
    else
        printfn "Ошибка: необходимо ввести ровно один символ. Попробуйте снова"
        readTargetCharacter ()

/// Подсчитывает количество файлов в каталоге (без подкаталогов).
/// имя которых начинается с указанного символа (с учетом регистра).
let countFilesStartingWith targetChar directoryPath =
    try
        // GetFiles возвращает массив имен файлов (включая пути), без подкаталогов.
        Directory.GetFiles(directoryPath)
        |> Array.map Path.GetFileName
        |> Array.filter (fun fileName ->
            not (String.IsNullOrEmpty(fileName)) && fileName.[0] = targetChar)
        |> Array.length
    with
    | :? UnauthorizedAccessException ->
        printfn "Ошибка доступа: нет прав на чтение каталога %s." directoryPath
        0
    | error ->
        printfn "Произошла непредвиденная ошибка: %s" error.Message
        0

[<EntryPoint>]
let main _ =
    // 1. Ввод пути с валидацией.
    let directoryPath = readDirectoryPath ()
    
    // 2. Ввод искомого символа с валидацией.
    let targetChar = readTargetCharacter ()
    
    // 3. Вызов функции подсчета.
    let resultCount = countFilesStartingWith targetChar directoryPath
    
    // 4. Вывод результата.
    printfn "\n--- Результат ---"
    printfn "Каталог: %s" directoryPath
    printfn "Искомый символ: '%c'" targetChar
    printfn "Количество файлов: %i" resultCount
    
    0