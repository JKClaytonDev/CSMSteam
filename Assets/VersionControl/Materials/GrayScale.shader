Shader "Unlit/GrayScale"
{
    Properties
    {
         _MainTex("Color (RGB) Alpha (A)", 2D) = "white"
        [MaterialToggle] _isFlipped("_isFlipped", int) = 1



    }
        SubShader
    {
    Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
    LOD 100


     Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert

            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            int _isFlipped;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture



                i.uv.x = ((1.0 - i.uv.x) * _isFlipped) + (i.uv.x * (1- _isFlipped));
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // apply fog

                col.g = col.r = col.b = (col.g + col.r + col.b)/3;

                return col;

            }
            ENDCG
        }
    }
}
