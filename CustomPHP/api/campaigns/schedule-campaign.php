<?php include('../_connect.php');?>
<?php include('../../includes/helpers/short.php');?>
<?php 
	
    //-------------------------- ERRORS -------------------------//
	$error_core = array('No data passed', 'API key not passed', 'Invalid API key');
	$error_passed = array('Username not passed', 'Invalid username');
	$error_subject = array('No subject given');
	$error_fromname = array('No from name given');
	//-----------------------------------------------------------//

	//--------------------------- POST --------------------------//
	//api_key
	if(isset($_POST['api_key'])) $api_key = $_POST['api_key'];
	else $api_key = null;
	
	//user_name
	if(isset($_POST['user_name'])) $user_name = $_POST['user_name'];
	else $user_name = null;

	//campaignid
	if(isset($_POST['campaign_id'])) $campaign_id = $_POST['campaign_id'];
	else $campaign_id = null;
	
	//send_date. this is a mktime format
	if(isset($_POST['send_date'])) $send_date = $_POST['send_date'];
	else $send_date = null;

	//email_lists
	if(isset($_POST['email_lists'])) $email_lists = $_POST['email_lists'];
	else $email_lists = null;

	//timezone
	if(isset($_POST['timezone'])) $timezone = $_POST['timezone'];
	else $timezone = null;

	//-----------------------------------------------------------//
	
	//----------------------- VERIFICATION ----------------------//

    //Core data
	if($api_key==null && $user_name==null)
	{
		echo $error_core[0];
		exit;
	}
    
	if($api_key==null)
	{
		echo $error_core[1];
		exit;
	}
	else if(!verify_api_key($api_key))
	{
		echo $error_core[2];
		exit;
	}
    
    //Passed data
	if($user_name==null)
	{
		echo $error_passed[0];
		exit;
	}

    $q = 'SELECT app FROM login WHERE username = "'.$user_name.'" and app is not null';
    $r = mysqli_query($mysqli, $q);
	if (mysqli_num_rows($r) == 0 || mysqli_num_rows($r) === NULL)
	{
    
		echo $error_passed[1]; 
		exit;
        
	}else{
		while($row = mysqli_fetch_array($r))
		{
			if($row['app'] === NULL)
			{
            
				echo $error_passed[1]; 
				exit;
                
			}
			$app_id = $row['app'];
		}  
	}
    
    //-----------------------------------------------------------//

	$q = 'UPDATE campaigns SET send_date = "'.$send_date.'", lists = "'.$email_lists.'", timezone = "'.$timezone.'" WHERE id = '.$campaign_id;
	$r = mysqli_query($mysqli, $q);
	if ($r)
	{
		// yey success
        echo "true";
	}
    else{
        echo "false";
    }

?>