namespace InternalLibrary.Mapping
{
    using AutoMapper;

    public interface IMapTo<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(GetType(), typeof(T));
        }
    }
}