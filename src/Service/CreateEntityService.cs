using WorldZero.Common.Interface.Entity;

// TODO: when done with this class, learn DI to make sure I don't fuck up the others
// also make ICreateEntityService
namespace WorldZero.Service
{
    // should these be async when talking to the repo? or should I make the repo methods async? or do i want them both to be async?
    //      then have non-async mehtods that just call the async methods and await them
    /*j
    public class CreateEntityService<T> where T : IEntity
    {
        private IGenericRepo<T> _repo;

        public CreateEntityService(IGenericRepo<T> repo)
        {
            this._repo = repo;
        }

        public void Create(T entity)
        {
            this._repo.Insert(entity);
        }
    }
    */
}