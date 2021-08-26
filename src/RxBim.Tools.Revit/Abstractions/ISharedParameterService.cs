﻿namespace RxBim.Tools.Revit.Abstractions
{
    using Autodesk.Revit.DB;
    using CSharpFunctionalExtensions;
    using Models;

    /// <summary>
    /// Сервис по работе с общими параметрами
    /// </summary>
    public interface ISharedParameterService
    {
        /// <summary>
        /// Добавление общего параметра из указанного ФОП в текущий документ. Если параметр уже существует, то метод выйдет без
        /// каких-либо действий над параметром. Проверка уже существующего параметра производится с учетом аргумента fullMatch.
        /// Метод позволяет работать как в существующей транзакции, так и с созданием новой транзакции
        /// </summary>
        /// <remarks>Метод создает транзакцию</remarks>
        /// <param name="definitionFile">ФОП</param>
        /// <param name="sharedParameterInfo">Данные об общем параметре</param>
        /// <param name="fullMatch">True - параметр ФОП должен совпасть со всеми заполненными
        /// значениями sharedParameterInfo. False - параметр ищется только по имени
        /// <para/>При поиске в ФОП, если задано true, происходит проверка по всем свойствам sharedParameterInfo, которые имеют значение
        /// <para/>При поиске в текущем документе, если задано true, происходит проверка только по свойствам:
        /// Имя, Guid, DataType. Если последние два имеют значение у sharedParameterInfo
        /// </param>
        /// <param name="useTransaction">Создавать транзакцию внутри метода</param>
        /// <returns>true - если параметр был добавлен</returns>
        Result AddSharedParameter(
            DefinitionFile definitionFile,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch,
            bool useTransaction = false);

        /// <summary>
        /// Метод добавляет общий параметр из указанного ФОП в текущий документ,
        /// дополняя привязки параметра категориями,
        /// указанными в <paramref name="sharedParameterInfo"/>.
        /// <see cref="SharedParameterCreateData.CategoriesForBind"/>.
        /// Проверка на присутствие существующего параметра
        /// производится с учетом аргумента fullMatch.
        /// </summary>
        /// <remarks>Метод не создает транзакцию</remarks>
        /// <param name="definitionFiles">Файлы общих параметров документа</param>
        /// <param name="sharedParameterInfo">Данные об общем параметре</param>
        /// <param name="fullMatch">True - параметр ФОП должен совпасть со всеми заполненными
        /// значениями sharedParameterInfo. False - параметр ищется только по имени
        /// <para/>При поиске в ФОП, если задано true, 
        /// происходит проверка по всем свойствам sharedParameterInfo, 
        /// которые имеют значение
        /// <para/>При поиске в текущем документе,
        /// если задано true, происходит проверка только по свойствам:
        /// Имя, Guid, DataType. Если последние два имеют значение у sharedParameterInfo
        /// </param>
        /// <returns>true - если параметр был добавлен или обновлён</returns>
        Result AddOrUpdateParameter(
            DefinitionFile[] definitionFiles,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch);

        /// <summary>
        /// Проверка параметра, представленного экземпляром <see cref="SharedParameterElement"/>, на существование
        /// в файле общих параметров
        /// </summary>
        /// <param name="definitionFile">ФОП</param>
        /// <param name="sharedParameterInfo">Данные об общем параметре</param>
        /// <param name="fullMatch">True - параметр ФОП должен совпасть со всеми заполненными
        /// значениями sharedParameterInfo. False - параметр ищется только по имени</param>
        Result ParameterExistsInDefinitionFile(
            DefinitionFile definitionFile,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch);

        /// <summary>
        /// Возвращает <see cref="DefinitionFile"/>, подключенный в текущем документе
        /// </summary>
        /// <param name="document">Документ, из которого требуется получить ФОП.
        /// Если задано null, то ФОП будет браться из текущего документа</param>
        Result<DefinitionFile> GetDefinitionFile(Document document = null);

        /// <summary>
        /// Считывает файлы общих параметров используя информацию
        /// из <see cref="SharedParameterFileSource"/>
        /// </summary>
        /// <param name="fileSource"><see cref="SharedParameterFileSource"/></param>
        DefinitionFile[] TryGetDefinitionFiles(SharedParameterFileSource fileSource);

        /// <summary>
        /// Проверяет существование параметра в документе
        /// </summary>
        /// <param name="definition">Данные об общем параметре</param>
        /// <param name="fullMatch">True - параметр должен совпасть со всеми заполненными значениями
        /// sharedParameterInfo, доступными для проверки через SharedParameterElement (Имя, Guid, DataType).
        /// False - параметр ищется только по имени</param>
        bool ParameterExistsInDocument(SharedParameterDefinition definition, bool fullMatch);
    }
}
