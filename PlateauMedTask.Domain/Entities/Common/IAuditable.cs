using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Domain.Entities.Common
{
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        /// <value>The creation time.</value>
        DateTime CreationTime { get; set; }
    }

    /// <summary>
    /// This interface is implemented by entities that is wanted to store creation information (who and when created).
    /// Creation time and creator user are automatically set when saving <see cref="Entity" /> to database.
    /// </summary>
    public interface ICreationAudited<T> : IHasCreationTime
    {
        /// <summary>
        /// Id of the creator user of this entity.
        /// </summary>
        /// <value>The creator user identifier.</value>
        T CreatorUserId { get; set; }
    }

    public interface IHasModificationTime
    {
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        /// <value>The last modification time.</value>
        DateTime? LastModificationTime { get; set; }
    }

    /// <summary>
    /// This interface is implemented by entities that is wanted to store modification information (who and when modified
    /// lastly).
    /// Properties are automatically set when updating the <see cref="IEntity" />.
    /// </summary>
    public interface IModificationAudited<T> : IHasModificationTime
    {
        /// <summary>
        /// Last modifier user for this entity.
        /// </summary>
        /// <value>The last modifier user identifier.</value>
        T? LastModifierUserId { get; set; }
    }

    public interface IHasDeletionTime
    {
        /// <summary>
        /// Deletion time of this entity.
        /// </summary>
        /// <value>The deletion time.</value>
        DateTime? DeletionTime { get; set; }
    }

    /// <summary>
    /// This interface is implemented by entities which wanted to store deletion information (who and when deleted).
    /// </summary>
    public interface IDeletionAudited<T> : IHasDeletionTime
    {
        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        /// <value>The deleter user identifier.</value>
        T DeleterUserId { get; set; }
    }

}
