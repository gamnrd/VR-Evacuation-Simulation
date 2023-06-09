$modelname "G:\Valve\models\SynthySpencer\valve.smd"
$scale 1.00
$cdmaterials "models"

$body "Body" "valve.smd"

$sequence "Idle" "G:\Valve\models\SynthySpencer\valve.smd" fps 30 ACT_IDLE 1

$surfaceprop "metal"

$keyvalues { "prop_data" {"base" "metal.small"}

$collisionmodel "G:\Valve\models\SynthySpencer\valve.smd" {
	$concave
	$mass 100.000
}
