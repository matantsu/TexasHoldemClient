using FireSharp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.BusinessLayer.api
{
    public static class RxFirebase
    {

        private static Dictionary<string, IObservable<object>> cache = new Dictionary<string, IObservable<object>>();

        public static IObservable<T> FromPath<T>(IFirebaseClient fb, string path)
        {
            if (cache.ContainsKey(path))
                return cache[path] as IObservable<T>;
            else { 
                IObservable<T> o = Observable.Create<T>(async s =>
                {
                    var sub = await fb.OnChangeGetAsync<T>(path, (_, x) => s.OnNext(x));
                    return Disposable.Create(() =>
                    {
                        sub.Dispose();
                    });
                }).Replay(1).RefCount();
                cache[path] = o as IObservable<object>;
                return o; 
            }
        }

        public static IObservable<B> FlatMap<A, B>(IFirebaseClient fb, IObservable<A> oba, Func<A, IObservable<B>> f)
        {
            return oba.SelectMany(f);
        }

        public static IObservable<IEnumerable<T>> FromPaths<T>(IFirebaseClient fb, IObservable<IEnumerable<string>> ob)
        {
            return Observable.Switch(ob.Select(xs => Observable.CombineLatest(xs.Select(path => FromPath<T>(fb, path)))));
        }

        public static IObservable<T> Trace<T>(string tag, IObservable<T> o)
        {
            return o.Do(x => Console.WriteLine(tag + " | " + JsonConvert.SerializeObject(x)));
        }

        public static ObservableCollection<T> ToCollection<T>(IObservable<IEnumerable<T>> o)
        {
            var c = new ObservableCollection<T>();
            o.Subscribe(ts =>
            {
                c.Clear();
                ts.ToList().ForEach(c.Add);
            });
            return c;
        }
    }
}
