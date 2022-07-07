#version 330 core
layout(location = 0) in vec2 aPosition;
layout(location = 1) in vec3 aColor;
layout(location = 2) in vec2 aTexCoord;

out vec4 vertexColor;
out vec2 TexCoord;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main() {
  vertexColor = vec4(aColor.rgb, 1.0);
  gl_Position = projection * view * model * vec4(aPosition.xy, 0, 1.0);
  TexCoord = aTexCoord;
}