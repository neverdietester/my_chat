namespace TrainingProgram.Entities.IEntity
{
    public interface IEntity<T>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public T Id { get; set; }
    }
}
