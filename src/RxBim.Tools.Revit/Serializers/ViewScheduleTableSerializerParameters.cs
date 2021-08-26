﻿namespace RxBim.Tools.Revit.Serializers
{
    using Tools.TableBuilder.Abstractions;

    /// <summary>
    /// Параметры сериализации в таблицу Revit
    /// </summary>
    public class ViewScheduleTableSerializerParameters : ITableSerializerParameters
    {
        /// <summary> Имя таблицы (Спецификации) </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id жирной линии
        /// </summary>
        public int? SpecificationBoldLineId { get; set; }
    }
}