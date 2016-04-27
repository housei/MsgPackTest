using UnityEngine;
using System.Collections;
using MsgPack;
using MsgPack.Serialization;

namespace Test
{
    public class Test01
    {
        public string id;
        public Test02 test02;
    }

    public class Test02
    {
        public string id;
    }

    public class TestMain : MonoBehaviour 
    {
    	void Start () {
            SerializationContext context = 
                new SerializationContext();

            // RegisterOverride Serializer
            var test_test01serializer = new Test_Test01Serializer(context);
            context.Serializers.RegisterOverride(test_test01serializer);

            var test_test02serializer = new Test_Test02Serializer(context);
            context.Serializers.RegisterOverride(test_test02serializer);

            context.CompatibilityOptions.PackerCompatibilityOptions = PackerCompatibilityOptions.None;

            MessagePackSerializer.Get<Test01>(context);

            Test01 test01 = new Test01(){ 
                id = "test01", 
                test02 = new Test02(){ id = "test02" }
            };

            byte[] bytes =
                MessagePackSerializer.Get<Test01>(context).PackSingleObject(test01);

            Test01 __test01 =
                MessagePackSerializer.Get<Test01>(context).UnpackSingleObject(bytes);
            
            Debug.Log(__test01.id + " " + __test01.test02.id);
    	}
    }
}