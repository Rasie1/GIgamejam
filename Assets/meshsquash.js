//	The direction along which the stretch/squash is performed...
var squashAxis: Vector3;

//	...and the scaling along that axis.
var scale: float;


private var mesh: Mesh;
private var origVerts: Vector3[];
private var verts: Vector3[];


function Start() {
//	Get the mesh data.
	var mf: MeshFilter = GetComponent(MeshFilter);
	mesh = mf.mesh;

//	Keep a copy of the original vertices and declare a new array of vertices.
	origVerts = mesh.vertices;
	verts = new Vector3[mesh.vertexCount];
}


function Update () {
	Squash(squashAxis, scale);
}


function Squash(axis: Vector3, factor: float) {
//	Create a new local coordinate space where one axis is the stretch/squash direction
//	and the other two are arbitrary. Call the axes R, G and B to avoid confusion with
//	the original X, Y and Z.
	var r: Vector3 = axis;
	var g: Vector3;
	var b: Vector3;

//	OrthoNormalize does all the work of creating the two axes perpendicular to the
//	stretch/squash axis!	
	Vector3.OrthoNormalize(r, g, b);
	
//	The matrix transforms points into the new coordinate space. Its inverse transforms
//	them back again.
	var mat: Matrix4x4;
	mat.SetRow(0, r);
	mat.SetRow(1, g);
	mat.SetRow(2, b);
	mat.SetRow(3, new Vector4(0, 0, 0, 1));
	var inv: Matrix4x4 = mat.inverse;
	
	for (i = 0; i < verts.Length; i++) {
	//	Convert the original unaltered vertex to the new coordinate space.
		var vert = mat.MultiplyPoint3x4(origVerts[i]);
		
	//	Change the vertex's position in the new space.
		vert.x *= factor;
	
	//	Convert it back to the usual XYZ space.
		verts[i] = inv.MultiplyPoint3x4(vert);
	}
	
//	Set the updated vertex array.
	mesh.vertices = verts;

//	The normals have probably changed, so let Unity update them.
	mesh.RecalculateNormals();
}